using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canTargetController : MonoBehaviour
{
    GameObject player;
    public float xRotation = 0;
    public float yRotation = 0;
    public Vector3 offset;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * 100 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 100 * Time.deltaTime;


        xRotation -= mouseY;

        yRotation -= mouseX;
        transform.position = player.transform.position + offset;
        transform.localRotation = Quaternion.Euler(xRotation, -yRotation, 0);
    }
}
