using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponAnimations : MonoBehaviour
{
    GameObject player;
    Animator playerAnim;
    Animator weaponAnim;

    public string currentState;

    // Состояния аниматора
    [SerializeField] bool _riffleIDLE;
    [SerializeField] bool _run;
    [SerializeField] bool _backRun;
    [SerializeField] bool _walk;
    [SerializeField] bool _standingJump;
    [SerializeField] bool _strafeLeft;
    [SerializeField] bool _strafeRight;
    [SerializeField] bool _scope;
    [SerializeField] bool _reladTrig;
    [SerializeField] bool _Kneel;

    // Animations
    string strafeLeft = "ak47 strafe left";
    string strafeRight = "weapon strafe right";
    string run = "ak 47 run";
    string scope = "scope";
    string kneel = "ak47 kneel";
    string walk = "ak47 walk";
    string jump = "ak 47 jump";
    string backward = "ak47 kneel backward";
    string kneelScope = "ak47 kneel scope";
    string kneelWalk = "ak47 kneel walk";
    string kneelBackward = "ak47 kneel backward";
    string weaponIDLE = "weapon IDLE";
    string backwardwalk = "weapon backward walk";
    string kneelStrafeLeft = "weapon kneel strafe left";
    string kneelStrafeRight = "weapon kneel strafe right";

    void Start()
    {
        player = GameObject.Find("Player");
        playerAnim = player.GetComponent<Animator>();
        weaponAnim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        // Обновление состояний аниматора
        _run = playerAnim.GetBool("isRun");
        _backRun = playerAnim.GetBool("isBackRun");
        _walk = playerAnim.GetBool("isWalk");
        _standingJump = playerAnim.GetBool("isStandingJump");
        _strafeLeft = playerAnim.GetBool("strafe left");
        _strafeRight = playerAnim.GetBool("strafe right");
        _scope = playerAnim.GetBool("isScope");
        //_reladTrig = playerAnim.GetBool("reloadTrig");
        _Kneel = playerAnim.GetBool("isKneel");
        _riffleIDLE = playerAnim.GetBool("riffleIDLE");

        if (!_Kneel)
        {

            if (_standingJump)
            {
                ChangeAnimationState(jump);
            }
            else if (_walk && !_run)
            {
                ChangeAnimationState(walk);
            }
            else if (_run)
            {
                ChangeAnimationState(run);
            }
            else if (_backRun)
            {
                ChangeAnimationState(backwardwalk);
            }
            else if (_strafeLeft)
            {
                ChangeAnimationState(strafeLeft);
            }
            else if (_strafeRight)
            {
                ChangeAnimationState(strafeRight);
            }
            else if (_scope)
            {
                ChangeAnimationState(scope);
            }
            else 
            {
                ChangeAnimationState(weaponIDLE);
            }
        }
        else
        {
            if (_walk)
            {
                ChangeAnimationState(kneelWalk);
            }
            else if (_backRun)
            {
                ChangeAnimationState(kneelBackward);
            }
            else if(_strafeRight)
            {
                ChangeAnimationState(kneelStrafeRight);
            }
            else if (_strafeLeft)
            {
                ChangeAnimationState(kneelStrafeLeft);
            }
            else if (_scope)
            {
                ChangeAnimationState(kneelScope);
            }
            else
            {
                ChangeAnimationState(kneel);
            }    
        }

           


    }

    public void ChangeAnimationState(string newState)
    {
        if (newState == currentState)
        {
            return;
        }

        

        weaponAnim.CrossFade(newState, .25f);
        currentState = newState;
    }

    public void PlayReload()
    {
        weaponAnim.SetTrigger("reloadTrig");
    }
}
