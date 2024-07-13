using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [HideInInspector] public bool _kneel;
    [HideInInspector] public bool _weaponActive;
    [HideInInspector] public bool isScope;
    [HideInInspector] public bool isInventoryOpen;
    [HideInInspector] public MOVE playerMove;
    [HideInInspector] public PlayerAnimation playerAnimation;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public playerLadderMove playerLadderMove;
    [HideInInspector] public GameObject camWeapon;
    [HideInInspector] public camWeaponScript camWeaponScript;
    public HandsAnimation handsAnimation;
    public Animator handsAnimator;
    [HideInInspector] public weapon playerWeapon;
    
    void Start()
    {
        _weaponActive = false;
        playerMove = GetComponent<MOVE>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerInput = GetComponent<PlayerInput>();
        playerLadderMove = GetComponent<playerLadderMove>();
    }

    // Update is called once per frame
    void Update()
    {
        _kneel = playerMove.isKneel;
    }
}
