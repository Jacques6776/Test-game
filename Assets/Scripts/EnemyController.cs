using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Enemy Movement controlls
    [SerializeField]
    private float minPatrolSpeed = 3f;
    [SerializeField]
    private float maxPatrolSpeed = 8f;
    private float startingSpeed;
    private float currentMoveSpeed;

    //Chase controlls
    [SerializeField]
    private float chaseSpeed = 4f;
    [SerializeField]
    private float engageDistance = 5f;

    //Collision controlls
    [SerializeField]
    private float totalStunTime = 1f;
    private float stunTimer;
    private bool isStunned = false;

    [SerializeField]
    private int totalEnemyAttacks = 3;
    private int enemyAttacks;

    private Rigidbody enemyRb;
    private PlayerController playerObject;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerObject = FindFirstObjectByType<PlayerController>();
        startingSpeed = Random.Range(minPatrolSpeed, maxPatrolSpeed);

        currentMoveSpeed = startingSpeed;

        stunTimer = totalStunTime;
    }

    private void Update()
    {
        if ((playerObject.transform.position - transform.position).magnitude < engageDistance)
        {
            ChaseState();            
        }

        if ((playerObject.transform.position - transform.position).magnitude > engageDistance)
        {
            PatrolState();
        }

        if (isStunned)
        {
            Debug.Log("Enemy stunned");
            stunTimer -= Time.deltaTime;

            if (stunTimer <= 0)
            {
                isStunned = false;
                stunTimer = totalStunTime;
                currentMoveSpeed = startingSpeed;
            }
        }
    }

    private void PatrolState()
    {
        enemyRb.linearVelocity = transform.forward * currentMoveSpeed;
    }

    private void ChaseState()
    {
        if (isStunned)
        {
            return;
        }
        else
        {
            Debug.Log("In range");
            Vector3 move = (playerObject.transform.position - transform.position).normalized;
            currentMoveSpeed = chaseSpeed;
            enemyRb.linearVelocity = move * currentMoveSpeed;
            transform.LookAt(playerObject.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            ObjectPool.ReturnObjectToPool(gameObject);
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            if (enemyAttacks < totalEnemyAttacks)
            {
                Debug.Log("Player attacked!");
                isStunned = true;
                enemyAttacks++;
            }
            else if (enemyAttacks >= totalEnemyAttacks)
            {
                ObjectPool.ReturnObjectToPool(gameObject);
                //Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, engageDistance);
    }
}
