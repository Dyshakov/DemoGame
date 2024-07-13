using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPrefabs : MonoBehaviour
{
    public GameObject wood;
    public GameObject weapon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetItemPrefabByName( string name )
    {
        switch (name )
        {
            case "wood":
                return wood;
            case "weapon":
                return weapon;
        }

        return null;
    }
}
