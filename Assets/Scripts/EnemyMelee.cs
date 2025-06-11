using UnityEngine;

public class EnemyMelee : EnemyController
{
    [SerializeField]
    private int damageAmount = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player attacked!");
            playerObject.DamagePlayer(damageAmount);
            isStunned = true;
            enemyAttacks++;
        }
    }
}
