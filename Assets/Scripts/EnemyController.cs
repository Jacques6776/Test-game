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
    protected bool isStunned = false;

    [SerializeField]
    protected int totalEnemyAttacks = 3;
    protected int enemyAttacks;
        
    protected bool inRange = false;

    private Rigidbody enemyRb;
    protected PlayerController playerObject;

    protected Quaternion startRotation;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerObject = FindFirstObjectByType<PlayerController>();
        startingSpeed = Random.Range(minPatrolSpeed, maxPatrolSpeed);

        currentMoveSpeed = startingSpeed;

        stunTimer = totalStunTime;

        startRotation = transform.rotation;
    }

    private void OnEnable()
    {
        transform.rotation = startRotation;
        enemyAttacks = 0;
        PatrolState();
    }

    private void Update()
    {
        if (enemyAttacks >= totalEnemyAttacks)
        {
            ObjectPool.ReturnObjectToPool(gameObject);
        }

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
        inRange = false;
        enemyRb.linearVelocity = transform.forward * currentMoveSpeed;
    }

    private void ChaseState()
    {
        inRange = true;
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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("Player"))
    //    {
    //        if (enemyAttacks < totalEnemyAttacks)
    //        {
    //            Debug.Log("Player attacked!");
    //            isStunned = true;
    //            enemyAttacks++;
    //        }
    //        else if (enemyAttacks >= totalEnemyAttacks)
    //        {
    //            transform.rotation = startRotation;
    //            ObjectPool.ReturnObjectToPool(gameObject);
    //            //Destroy(gameObject);
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            ObjectPool.ReturnObjectToPool(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, engageDistance);
    }
}
