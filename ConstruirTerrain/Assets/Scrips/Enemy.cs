using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseCombatEntity
{
    public Transform _enRespawn;
    public Transform _playertransform;

    public Material _nyaMaterial;
    public Material _defaultMaterial;

    public Renderer _Child;
    
    public NavMeshAgent _agent;
    public GameObject _projectile;
    public GameObject _shootposition;





   

    
    float myan = 0;

    

    public float sightRange;
    //navmesh
    
    // Estados
    private bool playerInSightRange;
    private bool distanceofplayer;
    private bool alreadyAttacked;
    bool nya = false;

    //patroling
    public Vector3 walkPoint;
    bool walkPointSet;

    [SerializeField]
    private float walkPointRange;
    [SerializeField]
    private float distanceRange;
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField] 
    private float Knock = 0.5f;



    // attack




    public LayerMask playerlayer, Whatisground;


    private void Start()
    {
        _playertransform = GameObject.Find("Lady").transform;
        _agent = GetComponent<NavMeshAgent>();
        maxHealth = 100;
        health = maxHealth;
    }
    void Update()
    {
      





        if (nya)
        {
            _Child.material = _nyaMaterial;
            myan += Time.deltaTime;
            if (myan > 0.25f)
                nya = false;
        
           
        }
        else
        {
            rb.velocity *= 0;
           
            _Child.material = _defaultMaterial;


            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerlayer);
            distanceofplayer = Physics.CheckSphere(transform.position, distanceRange, playerlayer);

            if (!playerInSightRange && !distanceofplayer)
            {
                Patrol();

            }
            if (playerInSightRange && !distanceofplayer)
            {
                FollowPlayer();
            }
            if (playerInSightRange && distanceofplayer)
            {
                AttackPlayer();
            }
        }
    }
    public void LateUpdate()
    {
        if (health <= 0)
        {
            health = maxHealth;
            transform.position = _enRespawn.position;

        }
    }



    protected void Patrol()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            _agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //si alcanzé el walkpoint...
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = !walkPointSet;
        }
    }

    protected void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, Whatisground))
        {
            walkPointSet = true;
        }
    }

    protected void FollowPlayer() 
    {
        _agent.SetDestination(_playertransform.position);
        
    }
    protected void AttackPlayer() 
    {
        _agent.SetDestination(transform.position);

        transform.LookAt(_playertransform);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        if (!alreadyAttacked)
        {

            // de momento lo que va a ser el ataque
            Rigidbody rb = Instantiate(_projectile, _shootposition.transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            rb.AddForce(transform.up, ForceMode.Impulse);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
      
        if(!nya)
            TakeDamage(20);
        nya = true;

          Transform parent = collision.gameObject.GetComponent<Sword>().parentTransform;
          Vector3 direction = transform.position - parent.position;
          direction.y = 0;
        rb.AddForce(direction.normalized * Knock, ForceMode.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceRange);

    }
}
