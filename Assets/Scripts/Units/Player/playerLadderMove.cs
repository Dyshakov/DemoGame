using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class playerLadderMove : MonoBehaviour
{
    [HideInInspector] public PlayerScript playerScript;
    [HideInInspector] public bool _onLadder;
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        bool _ladderUpper = Physics.Raycast(transform.position + Vector3.up * 1, transform.forward, out RaycastHit hit, 1);
        bool _ladderLower = Physics.Raycast(transform.position + Vector3.up * -1, transform.forward, out RaycastHit hit2, 1);

        if (_ladderUpper && hit.transform.CompareTag("ladder"))
        {
            _onLadder = true;
        }
        else if (_ladderLower && hit2.transform.CompareTag("ladder"))
        {
            _onLadder = true;
        }
        else
        { 
            _onLadder = false; 
        }
    }
}
