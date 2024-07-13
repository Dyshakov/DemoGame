using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;
    [SerializeField ] RectTransform healthBar;
    float healhBar_startLength = 250;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.sizeDelta = new Vector2(healhBar_startLength * playerHealth.health / 100, 50);
    }
}
