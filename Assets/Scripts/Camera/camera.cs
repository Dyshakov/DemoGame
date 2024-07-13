using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject character;
    public GameObject characterSpin;
    PlayerScript playerScript;
    [SerializeField] GameObject hands;
    public Vector3 offset;
    public float mouseSens = 200f;

    Ray ray;
    public float xRotation = 0;
    public float yRotation = 0;
    public RaycastHit hit;
    public GameObject camTarget;
    public bool isFirstView;
    bool lastView = true;
    bool isAltKeyDown = false;
    public float lastCamTargetRotationX;
    public float lastCamTargetRotationY;

    void Start()
    {
        playerScript = character.GetComponent<PlayerScript>();
        isFirstView = true;
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 10, Color.yellow);
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        if (!playerScript.isInventoryOpen)
        {
            xRotation -= mouseY;
            yRotation -= mouseX;
        }
        



        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawRay(transform.position, hit.point);
        }

        if (isFirstView)
        {
            transform.position = character.transform.position + offset;
            transform.localRotation = Quaternion.Euler(xRotation, -yRotation, 0);

        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.transform.rotation, .01f);
        }

        if (!isAltKeyDown)
        {
            character.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            characterSpin.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }


        if (Input.GetKeyDown(KeyCode.V))
        {
            PersonView();
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            lastCamTargetRotationX = camTarget.GetComponent<canTargetController>().xRotation;
            lastCamTargetRotationY = camTarget.GetComponent<canTargetController>().yRotation;
            isAltKeyDown = true;

        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            camTarget.GetComponent<canTargetController>().xRotation = lastCamTargetRotationX;
            camTarget.GetComponent<canTargetController>().yRotation = lastCamTargetRotationY;
            isAltKeyDown = false;

        }
    }


    void PersonView()
    {
        if (isFirstView)
        {
            ChangeLayer(character.transform, "Default");
            isFirstView = false;
            transform.SetParent(camTarget.transform, true);
            transform.localPosition = new Vector3(0, 2, -6);
            ChangeLayer(transform, "Weapon");
        }
        else
        {
            ChangeLayer(character.transform, "whatIsPlayer");
            isFirstView = true;
            transform.SetParent(null, true);
            ChangeLayer(transform, "camWeapon");
        }
    }

    public void ChangeLayer(Transform trans, string layerMaskName)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(layerMaskName);
        foreach (Transform child in trans)
        {
            ChangeLayer(child, layerMaskName);
        }
    }

    public void Scope()
    {
        if (!isFirstView || !lastView)
        {
            lastView = isFirstView;
            PersonView();
        }
    }

    public void ShowHands()
    {
        if (isFirstView)
        {
            hands.SetActive(true);
        }
    }

    public void HideHands()
    {
        hands.SetActive(false);
    }
}
