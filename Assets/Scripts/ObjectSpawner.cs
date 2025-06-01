using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //public GameObject gold;
    [SerializeField]
    private float spawnRadius = 0.1f;

    [SerializeField]
    private float spawnTimer;
    [SerializeField]
    private float minSpawnTime = 0.5f;
    [SerializeField]
    private float maxSpawnTime = 5f;

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        
        if (spawnTimer <= 0 )
        {
            SpawnObject();

            spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    private void SpawnObject()
    {
        GameObject gold = ObjectPool.sharedInstance.GetPooledObject();
        if(gold != null)
        {
            Vector2 spawnPostition = Random.insideUnitSphere * spawnRadius;
            gold.transform.position = new Vector3(spawnPostition.x, 0f, spawnPostition.y);
            gold.SetActive(true);
        }
        //Vector2 spawnPostition = Random.insideUnitSphere * spawnRadius;
        //Instantiate(gold, new Vector3(spawnPostition.x, 0f, spawnPostition.y), Quaternion.identity, this.transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
