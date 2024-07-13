using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryManager : MonoBehaviour
{
    public int maxItemsInStack = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject cameraObject;
    camera cameraScript;
    public GameObject activeItem;
    public GameObject camActiveItem;
    private GameObject mainInventory;
    GameObject player;
    PlayerScript playerScript;
    public GameObject playerSpine;
    [SerializeField] GameObject lootInventory;
    public bool isUseItem;
    public ItemType itemType;
    public InventorySound inventorySound;
    public AudioClip lootSound;

    public int selectedSlot = -1;

    void ChangeSelectedSlot(int newValue)
    {
        //���� ����� ����������� ����� �� �������� ��������
        if (selectedSlot != newValue)
        {
            if (selectedSlot >= 0)
            {
                inventorySlots[selectedSlot].DeSelect();
            }


            inventorySlots[newValue].Select();
            selectedSlot = newValue;
            GetSelectedItem();
        }
        //���� ���������� ���� ��� �������
        else
        {
            //������ ���������� ������ ����
            inventorySlots[selectedSlot].DeSelect();
            selectedSlot = -1;
            
            //������� �������� �������
            Destroy(activeItem);
            Destroy(camActiveItem);

            //��������� �������� ������ � �������
            playerScript._weaponActive = false;
            cameraScript.HideHands();
        }    
    }

    public void MoveItemInOtherSlot(InventorySlot inventorySlot)
    {
        // ���� ������������ ������� �������� �������� ���������
        if (inventorySlot == inventorySlots[selectedSlot])
            
        {
            inventorySlots[selectedSlot].DeSelect();    //������� ��������� �����
            Destroy(activeItem);                        // ������� �������� ������
            Destroy(camActiveItem);                     // ������� �������� ������� �� ������
            playerScript._weaponActive = false;         // �������� playerscript ��� ������ ������ ���������
            cameraScript.HideHands();                   //�������� ���� ��������� �� ������ ������� ����
        }
    }

    // ����� �������� � ���������, ���������� ������� 
    public InventoryItem FindItem(string itemName)
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.name == itemName)
            {
                return itemInSlot;  
            }
        }
        return null;
    }
        
    // Start is called before the first frame update
    void Start()
    {
        // ������ ������
        Cursor.visible = false;

        // ������������� ������ � ������ ������
        Cursor.lockState = CursorLockMode.Locked;

        inventorySound = GetComponent<InventorySound>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
        mainInventory = GameObject.Find("MainInventoryGroup");
        mainInventory.SetActive(false);
        cameraScript = cameraObject.GetComponent<camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ������ ������� I, �� ��������� ���������
        if(Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
        {
            mainInventory.SetActive(!mainInventory.active);
            lootInventory.SetActive(false);
            playerScript.isInventoryOpen = !playerScript.isInventoryOpen;
            ShowCursor();
        }

        // ����� � ���� �������� �� tool bar
        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            ChangeSelectedSlot(0);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            ChangeSelectedSlot(1);
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            ChangeSelectedSlot(2);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (selectedSlot >= 0 && itemType == ItemType.Item)
            {
                isUseItem = true;
                //inventorySlots[selectedSlot].UseItem();
            }
            
        }
        else
        {
            isUseItem = false;
        }
    }

    //��������� ������� � ���������
    public bool AddItem(item item, int count)
    {
        //���� ������� ������ �� �������� � ���������
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            //���� ���� �� ������, ������� � ����� ����� ��, ���������� �������� �� ��������� �������� ����� � ������� ���������
            if (itemInSlot != null
                && itemInSlot.item == item
                && itemInSlot.count < maxItemsInStack
                && itemInSlot.item.isStackable == true)
            {
                itemInSlot.count += count; //��������� ���������� �������� � ���������
                itemInSlot.RefreshCount(); // �������� ����� �������� � ���������
                return true;
            }
        }
        
        //���� ������ �������� � ��������� ���
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i]; // ����
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); // ������� � �����

            // ���� ���� ������
            if (itemInSlot == null)
            { 
                SpawnNewItem(item, slot, count);   //�������� ����� ������� � ���������
                if (item.type == ItemType.Weapon)
                {
                    slot.GetComponentInChildren<InventoryItem>().count = item.bullets;
                }
                return true;
            }
        }

            return false;
    }

    public void LootItem(item item)
    {
        inventorySound.PlaySound(lootSound);
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot, 1);
                break;
            }
        }
    }

    // ����� ������ �������� � ���������
    public void SpawnNewItem(item item, InventorySlot slot, int count)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        slot.GetComponent<InventorySlot>().item = newItemGo;
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
        inventoryItem.count = count;

        if (itemType == ItemType.Weapon)
        {
            inventoryItem.count = item.bullets;
        }
    }

    // ������� � ����� ������ �������� �������
    public item GetSelectedItem()
    {
        //������� �������� ������� �� ���
        Destroy(activeItem);
        Destroy(camActiveItem);

        InventorySlot slot = inventorySlots[selectedSlot];                       // �������� ����
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); // ������� � �������� �����
        // ���� ���� ������� � ����� � ��� �������� �� �������� ���������
        if (itemInSlot != null && itemInSlot.item.type != ItemType.bullets)
        {
            itemType = itemInSlot.item.type;    // ��� �������� � �����
            if(itemType == ItemType.Weapon)     // ���� ��� �������� ������
            {
                Debug.Log(inventorySlots[selectedSlot].GetComponentInChildren<InventoryItem>().count);
            }
            //������� �������� ������� � ����� ��������
            activeItem = Instantiate(itemInSlot.item.itemPrefab, player.transform);
            playerScript.playerWeapon = activeItem.GetComponent<weapon>();
            //���� ������� ��� �� �������� ����, �� �������� ��������� ����� ������� ��� ������
            if(!cameraScript.isFirstView)
            {
                activeItem.layer = LayerMask.NameToLayer("Default");
                foreach (Transform child in activeItem.transform)
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Default");
                }
            }

            camActiveItem = Instantiate(itemInSlot.item.itemCamPrefab, cameraObject.transform);
            playerScript.camWeapon = camActiveItem;
            //���� � ������ ������ ������� ��� �� �������� ����, �� ��������� ��������� ��� ������ ������
            if (!cameraScript.isFirstView)
            {
                cameraScript.ChangeLayer(camActiveItem.transform, "Weapon");
            }

            //���� ��� �������� ������, �� ����������� ���������� _weaponActive � ������� ������ ����������� �������� ��������, ��� ������������ ��������������� ��������
            if (itemInSlot.item.type == ItemType.Weapon)
            {
                playerScript._weaponActive = true;
                cameraScript.ShowHands();
            }
            else
            {
                cameraScript.HideHands();
                playerScript._weaponActive = false;
            }
        }
        else
        {
            cameraScript.HideHands();
            playerScript._weaponActive = false;
        }

        return null;
    }

    void ShowCursor()
    {
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
