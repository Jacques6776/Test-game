using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject gold;
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
        Vector2 spawnPostition = Random.insideUnitSphere * spawnRadius;
        Instantiate(gold, new Vector3(spawnPostition.x, 0f, spawnPostition.y), Quaternion.identity, this.transform);
    }
}
