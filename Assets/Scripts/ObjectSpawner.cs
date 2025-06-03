using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    [SerializeField]
    private float spawnRadius = 0.1f;
    
    [SerializeField]
    private float minSpawnTime = 0.5f;
    [SerializeField]
    private float maxSpawnTime = 5f;
    private float spawnTimer;

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
        Vector2 spawnPostition = Random.insideUnitSphere * spawnRadius;
        objectToSpawn.transform.position = new Vector3(spawnPostition.x, 0f, spawnPostition.y);

        ObjectPool.SpawnObject(objectToSpawn, objectToSpawn.transform.position, objectToSpawn.transform.rotation);

        //GameObject gold = ObjectPool.sharedInstance.GetPooledObject();
        //if(gold != null)
        //{
        //    Vector2 spawnPostition = Random.insideUnitSphere * spawnRadius;
        //    gold.transform.position = new Vector3(spawnPostition.x, 0f, spawnPostition.y);
        //    gold.SetActive(true);
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
