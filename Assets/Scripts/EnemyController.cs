using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Enemy Movement controlls
    [SerializeField]
    private float minPatrolSpeed = 3f;
    [SerializeField]
    private float maxPatrolSpeed = 8f;
    private float currentMoveSpeed;

    //Chase controlls
    [SerializeField]
    private float chaseSpeed = 4f;
    [SerializeField]
    private float engageDistance = 5f;

    private Rigidbody enemyRb;
    private PlayerController playerObject;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerObject = FindFirstObjectByType<PlayerController>();
        currentMoveSpeed = Random.Range(minPatrolSpeed, maxPatrolSpeed);
    }

    private void Update()
    {
        if ((playerObject.transform.position - transform.position).magnitude < engageDistance)
        {
            Debug.Log("In range");
            Vector3 move = (playerObject.transform.position - transform.position).normalized;
            enemyRb.linearVelocity = move * chaseSpeed;
            transform.LookAt(playerObject.transform);
        }

        if ((playerObject.transform.position - transform.position).magnitude > engageDistance)
        {
            PatrolState();
        }
    }

    private void PatrolState()
    {
        enemyRb.linearVelocity = transform.forward * currentMoveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, engageDistance);
    }
}
