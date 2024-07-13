using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;
    public GameObject item;
    public bool isActive = false;
    InventoryManager inventoryManager;

    private void Awake()
    {
        DeSelect();
    }

    public void Select()
    {
        isActive = true;
        image.color = selectedColor;
    }

    public void DeSelect()
    {
        isActive = false;
        image.color = notSelectedColor;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UseItem()
    {
        if(item)
        {
            // ���� ���������� �������� ����� 1, �� ������� ���� ������� �� ���������, �� ��� ������ � ������ ���� ����������
            if (GetComponentInChildren<InventoryItem>().count <= 1)
            {
                Destroy(item);
                Destroy(inventoryManager.activeItem);
                Destroy(inventoryManager.camActiveItem);
                DeSelect();
                inventoryManager.selectedSlot = -1;
            }
            // ����� ��������� ���������� �� ���� �������
            else
            {
                GetComponentInChildren<InventoryItem>().count -= 1;
                GetComponentInChildren<InventoryItem>().RefreshCount();   // ��������� ���������� �������� � ����������
            }
        }
    }
}
