using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    public GameObject player;
    public Transform playerBody;
    public Transform enemyEye;
    
    [Header("Attack Variables")]
    public float AttackDistance = 10.0f;

    private float attackTimer;
    [SerializeField]
    [Range(.1f,1.5f)]
    private float attackRefereshRate = 1.5f;

    public ParticleSystem muzzleFlash;

    public GameObject aimRifle;
    public GameObject Rifle;

    [Header("Variables For Hold Pos")]
    [SerializeField]
    [Range(0f,5f)]
    private float waitTime = 3f;

    private float moveTimer;

    [Header("WayPoint/Patrol Variables")]
    public float FollowDistance = 20.0f;
    public float AwareDistance = 20.0f;

    public Transform[] patrolPoints;

    private int currentControlPointIndex = 0;

    private float lookAtDistance = .5f;

    //For Bullet Graphics(VFX)
    [Tooltip("this is to get vfx implementation info")]
    [Header("Bullet VFX")]
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();

    private GameObject effectToSpawn;
    private GameObject fx;
    private Vector3 pos;

    private Vector3 direction;
    private Quaternion rotation;
    private Transform placedTransform;

    [Header("Sight")]
    public float fov = 120f;

    public bool inSight;

    public bool AwareOfPlayer;
    private bool aware;
    private int AwareVAriable;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        effectToSpawn = vfx[0];
        placedTransform = this.transform;
    }

    private void Update()
    {
        //Recoil For Enemy
        float x = Random.Range(-.5f, .5f);
        float y = Random.Range(.5f, 2);

        pos = new Vector3(x, y, 0);

       //Follow And Shoot Logic And Sight
        float distace = Vector3.Distance(player.transform.position, this.transform.position);
        float stopDistace = AttackDistance + 1;

        Vector3 playerDirection = player.transform.position - transform.position;
        float playerAngle = Vector3.Angle(transform.forward, playerDirection);

        if (playerAngle <= fov / 2 && AwareOfPlayer && distace < AwareDistance)
        {
            inSight = true;
        }
        else if (!AwareOfPlayer)
        {
            inSight = false;
        }
        else
        {
            inSight = false;
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
        if (distace <= stopDistace && AwareOfPlayer)
        {
            agent.stoppingDistance = AttackDistance;
            agent.speed = 0;
            animator.SetFloat("ShootSpeed", agent.velocity.magnitude);
            animator.SetBool("Shoot", true);
        }
        else
        {
            agent.speed = 5f;
            agent.stoppingDistance = 0;
            animator.SetBool("player", false);
            animator.SetBool("Shoot", false);
        }
        //For Chase Player
        if (distace < FollowDistance && FollowDistance > AttackDistance)
        {
            /* if (inSight && AwareVAriable == 0 && AwareOfPlayer)
             {
                 agent.SetDestination(player.transform.position);
                 animator.SetBool("player", true);
                 agent.speed = 5;
             }*/

            //agent.SetDestination(player.transform.position);
            //animator.SetBool("player", true);
            //agent.speed = 5;

            if (/*AwareVAriable == 1 &&*/ AwareOfPlayer)
            {
                agent.SetDestination(player.transform.position);
                animator.SetBool("player", true);
                agent.speed = 5;
            }
        }

        //If patrol point are zero then it return to originalPosition/placedPosition
        if (!AwareOfPlayer && patrolPoints.Length == 0)
        {
            agent.SetDestination(placedTransform.position);
        }

        if (distace < FollowDistance && AwareVAriable == 2)
        {
            if (patrolPoints.Length != 0)
            {
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    MoveToNextPatrolPoint();
                    agent.speed = 2;
                }
            }
        }
        else if (!agent.pathPending &&
            agent.remainingDistance < 0.5f && patrolPoints.Length != 0)
        {
            MoveToNextPatrolPoint();
            agent.speed = 2;
        }

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackRefereshRate)
        {
            attackTimer = 0;

            if (distace < AttackDistance && AwareOfPlayer)
            {
                Shoot();
            }
        }

        if (distace <= AttackDistance && distace > lookAtDistance && AwareOfPlayer)
        {
            transform.LookAt(player.transform);
        }

        if (distace <= AttackDistance)
        {
            aimRifle.gameObject.SetActive(true);
            Rifle.gameObject.SetActive(false);
        }
        else
        {
            aimRifle.gameObject.SetActive(false);
            Rifle.gameObject.SetActive(true);
        }

        //for draw ray for awre
        if (distace < AwareDistance)
        {
            DrawRay();
        }

       // Aware();
    }

    private void Shoot()
    {
        muzzleFlash.Play();

        if (firePoint != null)
        {
            fx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            RotateToMouseDirection(fx, player.transform.position + pos);
        }
        else
        {
            Debug.Log("No Fire Point Assigned");
        }
    }

    void RotateToMouseDirection(GameObject obj, Vector3 destination)
    {

        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }

    private void Aware()
    {
        //if (PlayerMovement.instance.isWalk == true || PlayerCtrl.instance.isCrouch == true)
        //{
        //    AwareVAriable = 0;
        //    aware = true;
        //}
        //else if (PlayerMovement.instance.isWalk == false || PlayerCtrl.instance.isCrouch == false)
        //{
        //    AwareVAriable = 1;
        //    aware = false;
        //}

        if (aware || AwareOfPlayer)
        {
            AwareVAriable = 2;
        }
    }

    private void DrawRay()
    {
        Vector3 playerDirection = playerBody.position - transform.position;
        RaycastHit hit;
        Debug.DrawRay(enemyEye.position, playerDirection, Color.magenta, .1f);
        if (Physics.Raycast(enemyEye.position, playerDirection , out hit))
        {
            if (hit.transform.tag == "Player")
            {
                AwareOfPlayer = true;
            }
            else
            {
                AwareOfPlayer = false;
            }
        }
    }

    void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length > 0)
        {
            agent.destination = patrolPoints[currentControlPointIndex].position;

            moveTimer += Time.deltaTime;
            if (canMove())
            {
                Move();
                currentControlPointIndex++;
                currentControlPointIndex %= patrolPoints.Length;
            }
        }
    }

    private void Move()
    {
        moveTimer = 0;
    }

    private bool canMove()
    {
        return moveTimer >= waitTime;
    }

    private void OnDrawGizmosSelected()
    {
        if (patrolPoints.Length > 0)
        {
            Gizmos.color = Color.yellow;
            Vector3 prev = patrolPoints[0].position;
            for (int n = 0; n < patrolPoints.Length; ++n)
            {
                Vector3 next = patrolPoints[(n + 1) % patrolPoints.Length].position;
                Gizmos.DrawLine(prev, next);
                prev = next;
            }
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, FollowDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, AttackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, player.transform.position);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, AwareDistance);
    }
}
