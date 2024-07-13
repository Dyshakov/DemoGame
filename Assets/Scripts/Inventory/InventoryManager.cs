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
        //Если номер выбираемого слота не является активным
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
        //Если выбираемый слот уже активен
        else
        {
            //Делаем неактивным данный слот
            inventorySlots[selectedSlot].DeSelect();
            selectedSlot = -1;
            
            //Убираем активный предмет
            Destroy(activeItem);
            Destroy(camActiveItem);

            //Выключаем анимацию игрока с оружием
            playerScript._weaponActive = false;
            cameraScript.HideHands();
        }    
    }

    public void MoveItemInOtherSlot(InventorySlot inventorySlot)
    {
        // Если перемещаемый предмет является активным предметов
        if (inventorySlot == inventorySlots[selectedSlot])
            
        {
            inventorySlots[selectedSlot].DeSelect();    //убираем выделение слота
            Destroy(activeItem);                        // убираем активный предет
            Destroy(camActiveItem);                     // убираем активный предмет из камеры
            playerScript._weaponActive = false;         // сообщаем playerscript что оружие больше неактивно
            cameraScript.HideHands();                   //скрываем руки персонажа из камеры первого лица
        }
    }

    // Поиск предмета в инвентаре, возвращает предмет 
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
        // Скрыть курсор
        Cursor.visible = false;

        // Заблокировать курсор в центре экрана
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
        // Если нажата клавиша I, то открываем инвентарь
        if(Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
        {
            mainInventory.SetActive(!mainInventory.active);
            lootInventory.SetActive(false);
            playerScript.isInventoryOpen = !playerScript.isInventoryOpen;
            ShowCursor();
        }

        // Берем в руки предметы из tool bar
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

    //добавляем предмет в инвентарь
    public bool AddItem(item item, int count)
    {
        //ищем наличие такого же предмета в инвентаре
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            //если слот не пустой, предмет в слоте такой же, количество предмета не превышает величину стака и предмет стакается
            if (itemInSlot != null
                && itemInSlot.item == item
                && itemInSlot.count < maxItemsInStack
                && itemInSlot.item.isStackable == true)
            {
                itemInSlot.count += count; //увеличить количество предмета в инвентаре
                itemInSlot.RefreshCount(); // поменять число предмета в инвентаре
                return true;
            }
        }
        
        //Если такого предмета в инвентаре нет
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i]; // слот
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); // предмет в слоте

            // Если слот пустой
            if (itemInSlot == null)
            { 
                SpawnNewItem(item, slot, count);   //добавить новый предмет в инвентарь
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

    // Спавн нового предмета в инвентаре
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

    // Спавним в руках игрока активный предмет
    public item GetSelectedItem()
    {
        //убираем активный предмет из рук
        Destroy(activeItem);
        Destroy(camActiveItem);

        InventorySlot slot = inventorySlots[selectedSlot];                       // активный слот
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); // предмет в активном слоте
        // если есть предмет в слоте и тип предмета не является патронами
        if (itemInSlot != null && itemInSlot.item.type != ItemType.bullets)
        {
            itemType = itemInSlot.item.type;    // тип предмета в слоте
            if(itemType == ItemType.Weapon)     // если тип предмета оружие
            {
                Debug.Log(inventorySlots[selectedSlot].GetComponentInChildren<InventoryItem>().count);
            }
            //Спавним активный предмет в руках модельки
            activeItem = Instantiate(itemInSlot.item.itemPrefab, player.transform);
            playerScript.playerWeapon = activeItem.GetComponent<weapon>();
            //Если текущий вид от третьего лица, то включаем видимость этого объекта для камеры
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
            //Если в данный момент активен вид от третьего лица, то выключаем видимость для оружия камеры
            if (!cameraScript.isFirstView)
            {
                cameraScript.ChangeLayer(camActiveItem.transform, "Weapon");
            }

            //Если тип предмета оружие, то присваиваем переменной _weaponActive в скрипте игрока присваиваем истинное значение, для проигрывание соответствующей анимации
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
