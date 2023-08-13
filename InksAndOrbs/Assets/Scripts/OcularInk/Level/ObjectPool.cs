using System.Collections;
using System.Collections.Generic;
using OcularInk.Characters.Protagonist;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance; // Singleton instance

    public BrushArea prefab; // The prefab to pool
    public int poolSize; // The number of objects to initially pool

    private List<BrushArea> pool; // The list to store pooled objects

    private void Awake()
    {
        Instance = this;
        pool = new List<BrushArea>();

        // Populate the pool with objects
        for (int i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(prefab);
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }

    public BrushArea GetObject()
    {
        // Find the first inactive object in the pool and return it
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                pool[i].gameObject.SetActive(true);
                return pool[i];
            }
        }

        return null;

        // // If no inactive object is found, create a new one and add it to the pool
        // var newObj = Instantiate(prefab);
        // pool.Add(newObj);
        // return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        // Deactivate the object and move it to a safe location
        obj.SetActive(false);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
    }
}
