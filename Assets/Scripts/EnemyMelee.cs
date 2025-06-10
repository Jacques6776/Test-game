using UnityEngine;

public class EnemyMelee : EnemyController
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player attacked!");
            isStunned = true;
            enemyAttacks++;

            //if (enemyAttacks < totalEnemyAttacks)
            //{
            //    Debug.Log("Player attacked!");
            //    isStunned = true;
            //    enemyAttacks++;
            //}
            //else if (enemyAttacks >= totalEnemyAttacks)
            //{
            //    transform.rotation = startRotation;
            //    ObjectPool.ReturnObjectToPool(gameObject);
            //    //Destroy(gameObject);
            //}
        }
    }
}
