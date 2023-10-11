using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.InputSystem.Processors;
//using UnityEngine.InputSystem.UI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    private Vector3 startPosition;
    public Animator animator;
    public GameObject damageZone;
    public bool dead = false;
    private Rigidbody rb;
    public AudioSource noises;

    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Patroling")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float areaThreshold = 1.0f; // Adjust the threshold value as needed


    [Header("Attacking")] 
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public float timeBeforeHitbox = .5f;
    public float hitboxForward = 1f;
    private Vector3 attackPoint;
    private bool attackAlternator = false;

    [Header("States")] 
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [Header("Animations")]
    private bool isIdle;


    private void Awake()
    {
        player = GameObject.Find("PlayerCharacter").transform;
        agent= GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        //Code for making the enemey die
        bool death = false;
        //Debug.Log(dead);
        if (dead == true)
        {
            if(death == false)
            {
                agent.SetDestination(transform.position);
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                animator.SetBool("dead", true);
                death = true;
            }
            return;
        }


        //Debug.Log(agent.destination);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Idle();

            if (IsAtStartPosition())
            {
                animator.SetBool("isWalking", false);
            }
            
        }
        if (playerInSightRange && !playerInAttackRange && !alreadyAttacked)
        {
            ChasePlayer();
            animator.SetBool("isWalking", true);
        }
        if (playerInSightRange && playerInAttackRange)
        {
            if (!alreadyAttacked)
            {
                attackPoint = transform.position;
            }

            AttackPlayer();

        }

        if (Time.timeScale == 1f)
            noises.UnPause();
        else
            noises.Pause();
        


    }

    private void Idle()
    {
        agent.SetDestination(startPosition);
        
        /*if (!isIdle)
        {
            animator.SetBool("isIdle", true);
            isIdle = true;
        }*/

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        /*Vector3 playerPosition = player.position;
        playerPosition.y = transform.position.y; // Set the y-axis position to be the same as the object's position
        transform.LookAt(playerPosition);*/
    }

    private void AttackPlayer()
    {
        agent.SetDestination(attackPoint);

        Vector3 playerPosition = player.position;
        playerPosition.y = transform.position.y; // Set the y-axis position to be the same as the object's position
        transform.LookAt(playerPosition);

        if (!alreadyAttacked)
        {
            

            if (attackAlternator)
            {
                animator.SetBool("attack1", true);
                attackAlternator = false;
            }
            else
            {
                animator.SetBool("attack2", true);
                attackAlternator = true;
            }

            Invoke("AttackHitbox", timeBeforeHitbox);
            Invoke("ResetAttackAnimation", .5f);
                        
            
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            alreadyAttacked = true;
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            IsStaggered();
            Invoke("IsRecovered", .5f);
        }
            
    }

    private void IsStaggered()
    {
        agent.SetDestination(transform.position);
    }

    private void IsRecovered()
    {
        agent.SetDestination(player.position);
    }

    private void AttackHitbox()
    {

        Vector3 spawnPosition = transform.position + (transform.forward * hitboxForward) + (transform.up * 1f);
        GameObject dmageZone = Instantiate(damageZone, spawnPosition, Quaternion.identity);
    }

    void ResetAttackAnimation()
    {
        animator.SetBool("attack1", false);
        animator.SetBool("attack2", false);
    }


    public bool IsAtStartPosition()
    {
        float distance = Vector3.Distance(agent.transform.position, startPosition);
        return distance <= areaThreshold;
    }

}


