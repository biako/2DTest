using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Utility class to replace original Instantiate() and Destory() methods.
public class GameObjectUtil : MonoBehaviour {

    // Each type of GameObject is stored in one pool. All the pools for different types of GameObjects are stored in the public static pools. The RecycleGameObject is used as the key of the pools dicitonary.

    private static Dictionary<RecycleGameObject, ObjectPool> pools = new Dictionary<RecycleGameObject, ObjectPool>();

    private static ObjectPool GetObjectPool(RecycleGameObject recycleGameObject) {
        ObjectPool pool = null;

        // If there is such GameObject as the key in the pools, get the pool with such key
        if (pools.ContainsKey(recycleGameObject)) {
            pool = pools[recycleGameObject];
        }

        // If there is no such GameObject as the key in the pools. Create a pool for such GameObject and add the pool into the pools dictionary.
        else {
            GameObject poolContainer = new GameObject(recycleGameObject.gameObject.name + "ObjectPool"); // Create a poolContainer to add the ObjectPool component.
            poolContainer.tag = "ObjectPool";
            pool = poolContainer.AddComponent<ObjectPool>(); // Add the ObjectPool component
            pool.prefab = recycleGameObject; // Set the pool type
            pools.Add(recycleGameObject, pool); // Add the pool to the pools dictionary.
        }
        return pool;
    }




    /* To substitute the original Instantiate()
       To determine if whether recyleGameObject is attached to the prefab or not. (Recyclable GameObject will have a recyleGameObject) If yes, get it from the pool. If no, call instantiate() direclty.*/

    public static GameObject Instantiate(GameObject prefab, Vector3 pos) {
        GameObject instance = null;
        RecycleGameObject recyleGameObject = prefab.GetComponent<RecycleGameObject>();

        // If recyclable, get the pool and get the object from the pool.
        if (recyleGameObject != null) {
            ObjectPool pool = GetObjectPool(recyleGameObject); // Get the pool from all the pools
            instance = pool.NextObject(pos).gameObject; // Get the object from the pool
            instance.transform.localScale = new Vector3 (Mathf.Abs(instance.transform.localScale.x), Mathf.Abs(instance.transform.localScale.y), Mathf.Abs(instance.transform.localScale.z)); // Reset the flip of the recycled object.
        }

        // If not recyclable, call instantitate directly.
        else {
            instance = GameObject.Instantiate(prefab);
            instance.transform.position = pos;       

        }
        return instance;
    }




    /* To substitute the original Destory() 
       To determine if whether recyleGameObject is attached to the prefab or not. (Recyclable GameObject will have a recyleGameObject) If yes, and call Shutdown(), i.e. set inactive  If no, call Destory() direclty.*/

    public static void Destroy(GameObject prefab) {
        RecycleGameObject recyleGameObject = prefab.GetComponent<RecycleGameObject>();

        // If recyclable, set the GameObject inactive
        if (recyleGameObject != null) {
            recyleGameObject.Shutdown();// Set the GameObject inactive                     
        }

        // If not recyclable, call destory directly.
        if (recyleGameObject == null) {
            GameObject.Destroy(prefab);
        }
    }

    public static void Destroy(GameObject[] prefabs) {       
        for (int i=0; i < prefabs.Length; i++) {
            Destroy(prefabs[i]);
        }
    }
}


   

  





