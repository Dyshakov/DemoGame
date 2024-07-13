using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    PlayerScript playerScript;
    camera cameraScript;
    float recoil;
    float recoilInKneel = 0.5f;
    float recoilStand = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraScript = GameObject.Find("Main Camera").GetComponent<camera>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Если игрок находится в приседе, то отдача меньше чем стоя
        if (playerScript.playerMove.isKneel)
        {
            recoil = recoilInKneel;
        }
        else
        {
            recoil = recoilStand;
        }
    }

    public void Recoil()
    {
        cameraScript.yRotation -= Random.Range(-recoil, recoil);
        cameraScript.xRotation -= recoil;
    }
}
