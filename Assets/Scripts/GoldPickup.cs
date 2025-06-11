using UnityEngine;

public class GoldPickup : MonoBehaviour    
{
    [SerializeField]
    private int collectablePoint = 1;
    [SerializeField]
    private int healAmount = -1;

    private PlayerController playerObject;

    private void Awake()
    {
        playerObject = FindFirstObjectByType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerObject.GoldPickupCounter();

            ObjectPool.ReturnObjectToPool(gameObject);
        }
    }
}
