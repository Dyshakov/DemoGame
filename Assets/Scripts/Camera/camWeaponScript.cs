using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camWeaponScript : MonoBehaviour
{
    [SerializeField] Animator anim;
    bool isScope;
    PlayerInput playerInput;
    [SerializeField] GameObject muzzleEffect;
    [SerializeField] GameObject muzzlePos;
    [SerializeField] GameObject gilza;
    [SerializeField] GameObject gilzaPos;
    void Start()
    {
        
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Scope();
        }

        if (playerInput._keyW && playerInput._keyLeftShift)
        {
            anim.SetBool("isRun", true);
            anim.SetBool("isWalk", false);
        }
        else if (playerInput._keyW)
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isWalk", false);
        }

        if (playerInput._reloadKey && !isScope)
        {
            //anim.SetTrigger("reloadTrig");
        }

    }

    void Scope()
    {
        if(!isScope)
        {
            isScope = true;
            anim.SetBool("isScope", true);
        }
        else
        {
            isScope = false;
            anim.SetBool("isScope", false);
        }
    }

    public void ShotPlay()
    {
        ChangeLayer(muzzleEffect.transform, gameObject.layer);
        Instantiate(muzzleEffect, muzzlePos.transform);
        GameObject gilzaObject = Instantiate(gilza, gilzaPos.transform.position, Quaternion.identity);
        gilzaObject.GetComponent<Rigidbody>().AddForce(transform.forward * 5, ForceMode.Impulse);

    }

    public void ChangeLayer(Transform trans, int layerMaskName)
    {
        trans.gameObject.layer = layerMaskName;
        foreach (Transform child in trans)
        {
            ChangeLayer(child, layerMaskName);
        }
    }

    public void Reload()
    {
        anim.SetTrigger("reloadTrig");
    }
}
