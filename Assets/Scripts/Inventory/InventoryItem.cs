using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    PlayerScript playerScript;
    public item item;
    public Image image;
    public TMP_Text countText;
    [HideInInspector] public Transform parentAfterDrag;
    public int count = 1;
    [SerializeField] InventoryManager inventoryManager;

    public void InitialiseItem(item newItem)
    {
        item = newItem;
        name = item.name;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        inventoryManager.MoveItemInOtherSlot(transform.parent.GetComponent<InventorySlot>());
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        InitialiseItem(item);
    }

    void Awake()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript._weaponActive)
        {
            countText.text= count.ToString();
        }
    }

    public void MoveInOtherSlot()
    {
        inventoryManager.LootItem(item);
        Destroy(gameObject);
    }
}
