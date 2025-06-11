using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 7f;

    private Rigidbody projectileRB;

    [SerializeField]
    private int damageAmount = 1;
    private PlayerController playerObject;

    private void Awake()
    {
        projectileRB = GetComponent<Rigidbody>();
        playerObject = FindFirstObjectByType<PlayerController>();
    }

    private void Update()
    {
        projectileRB.linearVelocity = transform.forward * projectileSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is hit");
            playerObject.DamagePlayer(damageAmount);
        }

        ObjectPool.ReturnObjectToPool(gameObject);
    }
}
