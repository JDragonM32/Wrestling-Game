using UnityEngine;
using UnityEngine.AI;

public class AIMutant : MonoBehaviour
{
    NavMeshAgent agent;
    Animator aiAnimator;
    [SerializeField] float attackRange, sightRange;
    [SerializeField] Transform player;
    [SerializeField] LayerMask playerLayer, groundLayer;
    [SerializeField] float patrolRange;
    Vector3 walkPoint;
    public float nextAttackTime;
    public float attackCooldown = 2f;

    public int AImaxHealth = 100;
    int AIcurrentHealth;

    bool PlayerInSightRange, PlayerInAttackRange;
    bool walkPointSet;
    bool alreadyAttacking = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        aiAnimator = GetComponentInChildren<Animator>();
        AIcurrentHealth = AImaxHealth;
    }

    private void Update()
    {
        PlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!PlayerInSightRange && !PlayerInAttackRange) Patrol();
        if (PlayerInSightRange && !PlayerInAttackRange) ChasePlayer();
        if (PlayerInSightRange && PlayerInAttackRange) AttackPlayer();

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= attackRange)
        {
            agent.isStopped = true;
            if (Time.time >= nextAttackTime)
                AttackPlayer();
                nextAttackTime = Time.time + attackCooldown;
        }
    }

    void ChasePlayer()
    {
        if (PlayerInSightRange)
        {
            agent.SetDestination(player.position);
        }
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacking)
        {
            aiAnimator.SetTrigger("IsAttacking");
            alreadyAttacking = true;
            Invoke(nameof(ResetAttack), 2f);
        }
        void ResetAttack()
        {
            alreadyAttacking = false;
        }
        Debug.Log("Attacking Player");
        aiAnimator.Play("Mutant Punch");
    }

    void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();

        Vector3 distanceToWalk = transform.position - walkPoint;
        if (distanceToWalk.magnitude < 1.0f) walkPointSet = false;

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
    }

    void ChangeHealth (int amount)
    {
        AIcurrentHealth = Mathf.Clamp(AIcurrentHealth + amount, 0, AImaxHealth);
        Debug.Log(AIcurrentHealth + "/" + AImaxHealth);
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-patrolRange, patrolRange);
        float randomX = Random.Range(-patrolRange, patrolRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.CheckSphere(walkPoint, 2.0f, groundLayer))
        {
            walkPointSet = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
