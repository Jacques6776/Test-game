using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    [SerializeField]
    private float spawnRadius = 0.1f;
    
    [SerializeField]
    private float minSpawnTime = 0.5f;
    [SerializeField]
    private float maxSpawnTime = 5f;
    private float spawnTimer;

    private void Awake()
    {
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }

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
        Vector3 spawnPostition = Random.insideUnitSphere * spawnRadius + this.transform.position; // Need vector 2/3
        Vector3 spawnCoordinates = new Vector3(spawnPostition.x, 1f, spawnPostition.z);

        int index = Random.Range(0, (objectsToSpawn.Length));
        GameObject obj = objectsToSpawn[index];
        
        //obj.transform.position = new Vector3(spawnPostition.x, 1f, spawnPostition.y);

        ObjectPool.SpawnObject(obj, spawnCoordinates, this.transform.rotation.normalized, ObjectPool.PoolType.Gameobject);

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
