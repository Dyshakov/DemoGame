using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class health : MonoBehaviour
{
    public float healthpoints;
    Animator animator;
    enemyController enemyController;
    BotInventory botInventory;
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponent<enemyController>();
        botInventory = GetComponent<BotInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthpoints < 0)
        {
            Destroy(enemyController.weaponObject);
            

            Component[] allComponents = GetComponents(typeof(Component));
            System.Type[] exludedTypes = new System.Type[] { typeof(Transform), typeof(Animator), typeof(health), typeof(BoxCollider) };
            Component[] filteredComponents = allComponents.Where(c => !exludedTypes.Contains(c.GetType())).ToArray();
            
            foreach (Component component in filteredComponents)
            {
                ((Behaviour)component).enabled = false;
            }
            botInventory.isLootable = true;

            animator.Play("riffle death");
        }
    }
}
