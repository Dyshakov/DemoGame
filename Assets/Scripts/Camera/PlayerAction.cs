using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    PlayerInput playerInput;
    public RaycastHit hit;
    public GameObject actionText;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] AudioClip lootSound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray  = new Ray(transform.position, transform.forward);

        // ������� ��� ray ������� 5
        if (Physics.Raycast(ray, out hit, 6))
        {
            // ���� ��� ����������� � ������
            if (hit.collider.gameObject.TryGetComponent<DoorOpen>(out DoorOpen doorOpen))
            {

                // ������ ����� ��������� �� open
                actionText.GetComponent<TextMeshProUGUI>().text = "press F to open door";
                // ���������� ��������� �� ������
                actionText.SetActive(true);

                // ��� ������� �� ������ ��������, ����� �����������
                if (playerInput._ActionButtonDown)
                {
                    doorOpen.Open();
                }
            }
            // ���� ��� ����������� � �������� ���������
            else if(hit.collider.gameObject.TryGetComponent<item2>(out item2 item))
            {
                // ������ ����� ��������� �� loot
                actionText.GetComponent<TextMeshProUGUI>().text = "press F for loot item";
                // ���������� ��������� �� ������
                actionText.SetActive(true);
                
                
                if (playerInput._ActionButtonDown)
                {
                    inventoryManager.AddItem(item.itemObject, item.count);
                    audioSource.PlayOneShot(lootSound);
                    Destroy(hit.collider.gameObject);
                }
            }
            else
            {
                actionText.SetActive(false);
            }
        }
        else
        {
            // ��������� �����������
            actionText.SetActive(false);
        }
    }
}
