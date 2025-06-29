using UnityEngine;

public class EnemyRanged : EnemyController
{
    [SerializeField]
    private GameObject enemyProjectile;
    [SerializeField]
    private Transform projectileSpawn;

    [SerializeField]
    private float shotCooldownTime = 1f;
    private float shotTimer = 0f;

    private void FixedUpdate()
    {
        if (inRange)
        {
            if (shotTimer >= shotCooldownTime)
            {
                FireAtPlayer();
            }
            else
            {
                shotTimer += Time.deltaTime;
            }
        }
    }

    private void FireAtPlayer()
    {
        transform.LookAt(playerObject.transform.position);
        ObjectPool.SpawnObject(enemyProjectile, projectileSpawn.position, projectileSpawn.rotation, ObjectPool.PoolType.Gameobject);
        isStunned = true;
        shotTimer = 0f;
        enemyAttacks++;
    }
}
