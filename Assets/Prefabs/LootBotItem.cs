using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBotItem : MonoBehaviour
{
    public BotInventory botInventory;
    InventoryManager inventoryManager;
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ������ �������� �� ��������� ����
    public void LootItem()
    {
        // ������� �������� � ��������� ������
        inventoryManager.LootItem(GetComponentInChildren<InventoryItem>().item);
        // � ��������� ���� ������ ���������� ��������� = 0
        botInventory.count = 0;
        // ������� ������� �� ��������� ����
        Destroy(GetComponentInChildren<InventoryItem>().gameObject);
        
    }
}
