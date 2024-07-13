
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    public GameObject testObject;

    private Animator animator;
    public NavMeshAgent agent;
    public GameObject projectile;
    public GameObject weaponObject;
    public weapon weaponScript;
    public Transform player;
    public RaycastHit hit;
    public Ray ray;
    Animator weaponAnim;
    float walkSpeed = 3.5f;
    float runSpeed = 7f;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttack;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, playerInDirectVision;

    float attackTime;
    float attackRate = 10f;
    bool isStrafe = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        weaponScript = weaponObject.GetComponent<weapon>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        weaponAnim = weaponObject.GetComponent<Animator>();
    }

    private void Update()
    {
        ray = new Ray(transform.position + new Vector3(0, 2, 0), transform.forward);
        

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(transform.position + new Vector3(0, 2, 0), transform.forward * 5, Color.green);
            if (hit.collider.gameObject.name == "Player")
            {
                playerInDirectVision = true;
            }
            else
            {
                playerInDirectVision = false;
            }
        }
        else
        {
            playerInDirectVision = false;
        }


        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange == false)
        {
            Patroling();
        }
        else if (playerInSightRange && !playerInAttackRange || playerInSightRange && !playerInDirectVision)
        {
            agent.speed = runSpeed;
            ChasePlayer();
        }
        else if (playerInAttackRange && playerInSightRange && attackTime + 1 / attackRate < Time.time && !alreadyAttacked && playerInDirectVision)
        {
            AttackPlayer();
            attackTime = Time.time;
        }
        else
        {
            transform.LookAt(player);
        }
    }

    private void Patroling()
    {
        animator.SetBool("isShot", false);
        weaponAnim.SetBool("isShot", false);
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        animator.SetBool("isShot", false);
        weaponAnim.SetBool("isShot", false);
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        
        agent.SetDestination(transform.position);

        transform.LookAt(player);


        animator.SetBool("isShot", true);
        weaponAnim.SetBool("isShot", true);
        weaponScript.Shot();

        Invoke(nameof(StopAttack), 1);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void StopAttack()
    {
        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttack);
    }
}
