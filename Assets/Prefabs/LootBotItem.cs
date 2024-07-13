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

    // Лутаем предметы из инвентаря бота
    public void LootItem()
    {
        // спавним предметы в инвентаре игрока
        inventoryManager.LootItem(GetComponentInChildren<InventoryItem>().item);
        // в инвентаре бота ставим количество предметов = 0
        botInventory.count = 0;
        // удаляем предмет из инвентаря бота
        Destroy(GetComponentInChildren<InventoryItem>().gameObject);
        
    }
}
