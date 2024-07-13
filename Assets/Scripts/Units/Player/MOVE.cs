using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOVE : MonoBehaviour
{
    private CharacterController characterController;
    float horizontalInput;
    float verticalInput;
    [HideInInspector] public bool isOnGround = true;
    private float speed;
    public float walkSpeed = 7f;
    public float runSpeed = 14f;
    float kneelSpeed = 5f;
    float gravity = -20f;
    float strafeSpeed = 7f;
    public float jumpHeight = 2f;
    public float standingJumpHeight = 2;
    public float runningJumpHeight = 3;
    LayerMask groundLayerMask;
    LayerMask objectLayerMask;
    float velocity;
    camera cameraController;
    [HideInInspector]public bool isKneel;
    BoxCollider playerBoxCollider;
    PlayerScript playerScript;
    bool _onLadder;
    Vector3 move;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip runSound;
    [SerializeField] AudioClip stepSound;

    Vector3 colliderSize = new Vector3(0.4794834f, 1.761537f, 0.3101669f);
    Vector3 colliderCenter = new Vector3(-0.003294897f, 0.8730719f, 0.009854575f);

    Vector3 kneelColliderSize = new Vector3(0.4794834f, 1.126983f, 0.3101669f);
    Vector3 kneelColliderCenter = new Vector3(-0.003294897f, 0.5558381f, 0.00985457f);

    float playerHeight = 1.85f;
    float kneelPlayerHeight = 1.11f;
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        playerBoxCollider = GetComponent<BoxCollider>();
        speed = walkSpeed;
        isKneel = false;
        cameraController = GameObject.Find("Main Camera").GetComponent<camera>();
        characterController = GetComponent<CharacterController>();
        groundLayerMask = LayerMask.GetMask("whatIsGround");
        objectLayerMask = LayerMask.GetMask("Default");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = playerScript.playerInput.horizontal;
        verticalInput = playerScript.playerInput.vertical;

        //Бег
        if (playerScript.playerInput._keyLeftShift && !isKneel && playerScript.playerInput._keyW && !playerScript.isScope)
        {
            jumpHeight = runningJumpHeight;
            speed = runSpeed;
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            
        }
        else if (!playerScript.playerInput._keyLeftShift && !isKneel)
        {
            speed = walkSpeed;
            jumpHeight = standingJumpHeight;

            if(verticalInput > 0 && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(stepSound);
            }
            
            if(verticalInput <= 0)
            {
                audioSource.Stop();
            }
        }

        if (!playerScript.playerLadderMove._onLadder)
        {
            // Если игрок не на лестнице
            move = transform.right * horizontalInput * strafeSpeed + transform.forward * verticalInput * speed;
        }
        else
        {
            // Если игрок на лестнице
            move = transform.up * verticalInput * speed + transform.right * horizontalInput * strafeSpeed + transform.forward * verticalInput * speed;
        }
        
        characterController.Move(move * Time.deltaTime);

        // Присед
        if (playerScript.playerInput._keyLeftControlDown)
        {

            if (!isKneel)
            {
                isKneel = true;
                speed = kneelSpeed;
                characterController.height = kneelPlayerHeight;
                playerBoxCollider.size = kneelColliderSize;
                playerBoxCollider.center = kneelColliderCenter;
                characterController.center = new Vector3(0, 0.58f, 0);
                cameraController.offset = new Vector3(0, 2.7f, 0);
            }
            else
            {
                isKneel = false;
                speed = walkSpeed;
                cameraController.offset = new Vector3(0, 4.35f, 0);
                characterController.height = playerHeight;
                characterController.center = new Vector3(0, 0.91f, 0);
                playerBoxCollider.size = colliderSize;
                playerBoxCollider.center = colliderCenter;
            }

        }

        // Прыжок и гравитация
        isOnGround = Physics.Raycast(transform.position, -transform.up, 0.2f, groundLayerMask | objectLayerMask);
        Debug.DrawRay(transform.position, -transform.up * 0.2f, Color.red);

        // Если игрок находится на земле и нажата клавиша пробел, то совершаем прижок
        if (playerScript.playerInput._keySpace && isOnGround)
        {
            velocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        // Если игрок на земле или на лестнице, то скорость падения 0
        else if (isOnGround || playerScript.playerLadderMove._onLadder)
        {
            velocity = 0;
        }

        // Если игрок не на земле и не на лестнице, то увеличиваем скорость падения
        if (!isOnGround && !playerScript.playerLadderMove._onLadder)
        {
            velocity += gravity * Time.deltaTime;
        }

        characterController.Move(Vector3.up * velocity * Time.deltaTime);

        _onLadder = playerScript.playerLadderMove._onLadder;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
