using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class first_aid : MonoBehaviour
{
    GameObject player;
    PlayerInput playerInput;
    PlayerHealth playerHealth;
    [SerializeField] float healthHeal;
    InventoryManager inventoryManager;
    [SerializeField] AudioClip use_sound;
    void Start()
    {
        player = GameObject.Find("Player");
        playerInput = player.GetComponent<PlayerInput>();
        playerHealth = player.GetComponent<PlayerHealth>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryManager.isUseItem)
        {
            // ����������� ���� ������������� �������
            inventoryManager.inventorySound.PlaySound(use_sound);
            // ����������� �������� ������
            playerHealth.health += healthHeal;
            // ���������� ������� � �����
            inventoryManager.inventorySlots[inventoryManager.selectedSlot].UseItem();
        }
    }
}
