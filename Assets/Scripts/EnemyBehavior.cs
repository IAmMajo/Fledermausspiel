using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
tutorial: https://www.youtube.com/watch?v=UjkSFoLxesw
*/
public class EnemyBehavior : MonoBehaviour
{
    public Transform weapon;
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInAttackRange) Patroling(); 
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            
            TakeDamage(1);
        }
    }
    private void Patroling()
    {
        if(playerInSightRange)
        {
            if (!walkPointSet) SearchWalkPoint();
            if(walkPointSet && new Vector2(walkPoint.x-player.position.x,walkPoint.z-player.position.z).magnitude<new Vector2(transform.position.x-player.position.x,transform.position.z-player.position.z).magnitude) agent.SetDestination(walkPoint);
            else SearchWalkPoint();
        }
        else
        {
            if (!walkPointSet) SearchWalkPoint();
        
        if (walkPointSet) agent.SetDestination(walkPoint);

        }
        
        Vector2 distanceToWalkPoint = new Vector2(transform.position.x, transform.position.z) - new Vector2(walkPoint.x, walkPoint.z);
        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, whatIsGround))
            walkPointSet = true;
    }

    private void AttackPlayer()
    {
        
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            float randomY = Random.Range(-.02f,.02f);
            float randomX = Random.Range(-.02f,.02f);
            //Attack code here
            Rigidbody rb = Instantiate(projectile, weapon.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce((transform.forward + new Vector3(randomX,randomY,0)) * 50f, ForceMode.Impulse);
            //End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}