using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //It is static so we can use it in the static class at the bottom
    public static List<PooledObjectInfo> objectPools = new List<PooledObjectInfo>();

    //Actual method to spawn objects
    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        //this is a lamda expression, need to go read up more on this
        PooledObjectInfo pool = objectPools.Find(p => p.LookUpString == objectToSpawn.name);

        //If the pool doesn't exist, we will create one
        if(pool == null)
        {
            //Will save string of the object to the lookupstring so we can now find a pool for it
            pool = new PooledObjectInfo() { LookUpString = objectToSpawn.name };
            objectPools.Add(pool);
        }

        //Check if there are inactive objects instire the pool (this is after it is confirmed the pool exists)
        GameObject spawnableObject = null;
        foreach (GameObject obj in pool.InactiveObjects)
        {
            if (obj !=null)
            {
                spawnableObject = obj;
                break;
            }
        }

        if (spawnableObject == null)
        {
            //If there are no inactive objects, this will create a new one for use
            spawnableObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
        }
        else
        {
            //If there is an inactive object, activate it
            spawnableObject.transform.position = spawnPosition;
            spawnableObject.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);
        }

        return spawnableObject;
    }

    //This will set the object to inactive once it has been "destroyed"
    public static void ReturnObjectToPool(GameObject obj)
    {
        //All spawned objects will have a (clone) section at the end of its name. Need to remove that portion for this to work
        //See if there is a better way
        string goName = obj.name.Substring(0, obj.name.Length -7);
        //Another lamda thing
        PooledObjectInfo pool = objectPools.Find(p => p.LookUpString == goName);

        //This will warn that there is no such pool
        if (pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }

    //public static ObjectPool sharedInstance;
        
    //public GameObject objectToPool;
    //public List<GameObject> pooledObjects;
    //public int amountToPool;

    //private void Awake()
    //{
    //    sharedInstance = this;
    //}

    //private void Start()
    //{
    //    pooledObjects = new List<GameObject>();
    //    GameObject tmp;
    //    for (int i = 0; i < amountToPool; i++)
    //    {
    //        tmp = Instantiate(objectToPool, this.transform);
    //        tmp.SetActive(false);
    //        pooledObjects.Add(tmp);
    //    }
    //}

    //public GameObject GetPooledObject()
    //{
    //    for(int i = 0; i < amountToPool; i++)
    //    {
    //        if (!pooledObjects[i].activeInHierarchy)
    //        {
    //            return pooledObjects[i];
    //        }
    //    }
    //    return null;
    //}
}

//Class for different object pools. Contains lookup string and a list for the pooled objects
//Each time we create a new class, we will have a new pool of objects
public class PooledObjectInfo
{
    public string LookUpString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
