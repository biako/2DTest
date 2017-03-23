﻿using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {
    public GameObject explosion;		// Prefab of explosion effect.
    public string coltag;

    void Awake() {


    }

    void OnExplode() {
        // Create a quaternion with a random rotation in the z-axis.


        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        // Instantiate the explosion where the rocket is with the random rotation.
        Instantiate(explosion, transform.position, randomRotation);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        coltag = collision.gameObject.tag;
        OnExplode();
        GameObjectUtil.Destroy(gameObject);
        // If it hits the player...

        if (collision.gameObject.tag == "Player") {
            // ... find the Player script and call the Hurt function.
            //collision.gameObject.GetComponent<HealthHurt>().Hurt();
            GameObjectUtil.GameOver();
        }           

    }

}


