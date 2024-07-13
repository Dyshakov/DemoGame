using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public bool _keyW;
    [HideInInspector] public bool _keyA;
    [HideInInspector] public bool _keyS;
    [HideInInspector] public bool _keyD;
    [HideInInspector] public bool _keyLeftShift;
    [HideInInspector] public bool _keySpace;
    [HideInInspector] public bool _keyLeftControlDown;
    [HideInInspector] public bool _ActionButtonDown;
    [HideInInspector] public bool _reloadKey;
    [HideInInspector] public bool _shotKey;
    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.W))
        {
            _keyW = true;
        }
        else
        {
            _keyW = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _keyA = true;
        }
        else { _keyA = false; }

        if (Input.GetKey(KeyCode.S))
        {
            _keyS = true;
        }
        else
        {
            _keyS = false;
        }

        if(Input.GetKey(KeyCode.D))
        {
            _keyD = true;
        }
        else
        {
            _keyD = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _keyLeftShift = true;
        }
        else
        {
            _keyLeftShift= false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _keySpace = true;
        }
        else
        {
            _keySpace = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _keyLeftControlDown = true;
        }
        else
        {
            _keyLeftControlDown = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _ActionButtonDown = true;
        }
        else
        {
            _ActionButtonDown = false;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            _reloadKey = true;
        }
        else
        { 
            _reloadKey = false; 
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            _shotKey = true;
        }
        else
        {
            _shotKey = false;
        }
            
    }
}
