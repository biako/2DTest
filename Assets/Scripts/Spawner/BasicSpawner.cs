using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BasicSpawner for initiationg of Singleton Instance. Only one instance will exist at one time.
public class BasicSpawner : MonoBehaviour {
    public GameObject[] prefabs; // Array to store GameObject prefabs for inititation
    public bool active = false; // Whether spawner timer is active
    //public bool singleGenerated = false; // Whether the Single Instance is generated

    void Update() {
       
        if (active) { // If Spanwer time is active and the Single Instance has not been generated
            Transform newTransform = transform;
            GameObjectUtil.Instantiate(prefabs[0], newTransform.position); // inititate at the plase of the spawner itself
            //singleGenerated = true;
            active = false; // Set the spanwer inactive
        }

    }


}
