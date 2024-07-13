using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootManager : MonoBehaviour
{
    [SerializeField] GameObject lootInventory;
    PlayerAction playerAction;
    PlayerInput playerInput;
    GameObject player;
    PlayerScript playerScript;
    RaycastHit hit;
    [SerializeField] GameObject mainInventory;
    InventoryManager inventoryManager;
    [SerializeField] GameObject actionText;
    void Start()
    {
        lootInventory = GameObject.Find("LootInventory");
        lootInventory.SetActive(false);
        player = GameObject.Find("Player");
        playerInput = player.GetComponent<PlayerInput>();
        playerAction = GameObject.Find("Main Camera").GetComponent<PlayerAction>();
        playerScript = player.GetComponent<PlayerScript>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        hit = playerAction.hit;
        if (hit.collider && hit.collider.gameObject.TryGetComponent<BotInventory>(out BotInventory botInventory))
        {
            actionText.GetComponent<TextMeshProUGUI>().text = "press F for loot";
            actionText.SetActive(true);
            if(botInventory.isLootable && playerInput._ActionButtonDown)
            {
                Cursor.visible = !Cursor.visible;
                Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;

                playerScript.isInventoryOpen = !playerScript.isInventoryOpen;
                mainInventory.SetActive(!mainInventory.activeSelf);
                lootInventory.SetActive(!lootInventory.activeSelf);
                if (botInventory.slot.GetComponentInChildren<InventoryItem>())
                {
                    
                    Destroy(botInventory.slot.gameObject.GetComponentInChildren<GameObject>());
                }
                
                if (botInventory.item2 != null && botInventory.count > 0)
                {
                    botInventory.slot.GetComponent<LootBotItem>().botInventory = botInventory;
                    inventoryManager.SpawnNewItem(botInventory.item2, botInventory.slot, 1);
                }
                
            }
        }
        else
        {
            
        }
    }
}
