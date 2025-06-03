using UnityEngine;

public class GoldPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ObjectPool.ReturnObjectToPool(gameObject);
            //gameObject.SetActive(false);
        }
    }
}
