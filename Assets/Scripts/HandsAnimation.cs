using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAnimation : MonoBehaviour
{
    PlayerInput playerInput;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput._reloadKey)
        {
            //animator.SetTrigger("reloadTrig");
        }

        if (playerInput._keyW && playerInput._keyLeftShift)
        {
            animator.SetBool("isRun", true);
            animator.SetBool("isWalk", false);
        }
        else if (playerInput._keyW)
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isWalk", false);
        }

    }

    public void ShotPlay()
    {
        animator.Play("handsShot");
    }

    public void Reload()
    {
        animator.SetTrigger("reloadTrig");
    }
}
