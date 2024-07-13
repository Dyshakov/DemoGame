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

        // Создаем луч ray длинной 5
        if (Physics.Raycast(ray, out hit, 6))
        {
            // Если луч встречается с дверью
            if (hit.collider.gameObject.TryGetComponent<DoorOpen>(out DoorOpen doorOpen))
            {

                // Меняем текст подсказки на open
                actionText.GetComponent<TextMeshProUGUI>().text = "press F to open door";
                // Отображаем подсказку на экране
                actionText.SetActive(true);

                // При нажатии на кнопку действия, дверь открывается
                if (playerInput._ActionButtonDown)
                {
                    doorOpen.Open();
                }
            }
            // Если луч встречается с лутаемым предметом
            else if(hit.collider.gameObject.TryGetComponent<item2>(out item2 item))
            {
                // Меняем текст подсказки на loot
                actionText.GetComponent<TextMeshProUGUI>().text = "press F for loot item";
                // Отображаем подсказку на экране
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
            // Подсказка выключается
            actionText.SetActive(false);
        }
    }
}
