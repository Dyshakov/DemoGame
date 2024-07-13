using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]

public class item : ScriptableObject
{
    public TileBase tile;
    public Sprite image;
    public ItemType type;
    public ActionType actionType;
    public bool isStackable;
    public GameObject itemPrefab;
    public GameObject itemCamPrefab;
    public int bullets;
}

public enum ItemType {
    BuildingBlock,
    Tool,
    Weapon,
    Item,
    bullets
}

public enum ActionType
{
    Dig, Mine
}


