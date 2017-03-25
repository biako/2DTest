using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The ObjectPool will be attached to a PoolContainer automatically 
public class ObjectPool : MonoBehaviour {
    
    public RecycleGameObject prefab;
    private List<RecycleGameObject> pool = new List<RecycleGameObject>();


    private RecycleGameObject CreateInstance(Vector3 pos) {
        RecycleGameObject newInstance = GameObject.Instantiate(prefab); // Instantiate the GameObject
        newInstance.transform.position = pos; // Set the position using the pos parameter
        newInstance.transform.parent = transform; // Set the PoolContainer (where the ObjectPool is attached) as its parent
        pool.Add(newInstance); // Add to the pool
        return newInstance;
    }


    public RecycleGameObject NextObject(Vector3 pos) {
        RecycleGameObject instance = null;

        // If there is an instance in the pool, get the instance from the pool
        foreach (RecycleGameObject poolInstance in pool) {
            if (!poolInstance.gameObject.activeSelf) { // If the instance is inactive.
                instance = poolInstance;
                instance.transform.position = pos;
            }
        }

        // If there is no such instance in the pool, call CreateInstance above: return a new insance and add to the pool
        if (instance == null) instance = CreateInstance(pos);

        instance.Restart(); // Set the instance active
        return instance;
    }

    public RecycleGameObject GetInactiveObject() {
        RecycleGameObject instance = null;
        // If there is an instance in the pool, get the instance from the pool
        foreach (RecycleGameObject poolInstance in pool) {
            if (!poolInstance.gameObject.activeSelf) { // If the instance is inactive.
                instance = poolInstance;             
            }
        }
        return instance;
    }



}
