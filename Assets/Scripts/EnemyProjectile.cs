using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 7f;

    private Rigidbody projectileRB;

    private void Awake()
    {
        projectileRB = GetComponent<Rigidbody>();
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
        }

        Destroy(gameObject);
    }
}
