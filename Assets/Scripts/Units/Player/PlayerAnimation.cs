using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator playerAnim;
    PlayerScript playerScript;
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerScript = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.playerInput._keyLeftShift && !playerScript._kneel && playerScript.playerInput._keyW)
        {
            playerAnim.SetBool("isRun", true);
        }
        else
        {
            playerAnim.SetBool("isRun", false);
        }

        // ��������

        // ���� � ����� ������ ���� ������, �� ����������� �������� ������ � �������
        if (playerScript._weaponActive)
        {
            playerAnim.SetBool("riffleIDLE", true);
        }
        else
        {
            playerAnim.SetBool("riffleIDLE", false);
        }

        //��� ������� 'W' ����������� �������� �������� ������
        if (playerScript.playerInput._keyW)
        {
            playerAnim.SetBool("isWalk", true);
            playerAnim.SetBool("isBackRun", false);
            playerAnim.SetBool("strafe right", false);
            playerAnim.SetBool("strafe left", false);
        }
        else if (playerScript.playerInput._keyS)
        {
            playerAnim.SetBool("isWalk", false);
            playerAnim.SetBool("isBackRun", true);
            playerAnim.SetBool("strafe right", false);
            playerAnim.SetBool("strafe left", false);
        }
        else if (playerScript.playerInput._keyD)
        {
            playerAnim.SetBool("strafe right", true);
            playerAnim.SetBool("isWalk", false);
            playerAnim.SetBool("isBackRun", false);
            playerAnim.SetBool("strafe left", false);
        }
        else if (playerScript.playerInput._keyA)
        {
            playerAnim.SetBool("strafe left", true);
            playerAnim.SetBool("strafe right", false);
            playerAnim.SetBool("isWalk", false);
            playerAnim.SetBool("isBackRun", false);
        }
        else
        {
            playerAnim.SetBool("isWalk", false);
            playerAnim.SetBool("isBackRun", false);
            playerAnim.SetBool("strafe right", false);
            playerAnim.SetBool("strafe left", false);
        }

        if (playerScript.playerInput._keyLeftControlDown)
        {
            playerAnim.SetBool("isKneel", !playerAnim.GetBool("isKneel"));
        }

        if (playerScript.playerInput._keySpace)
        {
            playerAnim.SetBool("isStandingJump", true);
        }
        else if (playerScript.playerMove.isOnGround)
        {
            playerAnim.SetBool("isStandingJump", false);
        }

    }
}
