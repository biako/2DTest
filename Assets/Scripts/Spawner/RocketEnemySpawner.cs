using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Inherited from BasicSpawner. BasicSpawner can initiate only a Singleton instance. RocketEnemySpawner can initiate multiple instances at random intervals within a given range of delaytime
public class RocketEnemySpawner : BasicSpawner {

    public Vector2 delayRange = new Vector2(3, 5); // Range of delay time
    public float delay = 0; // Delay time of spawning


    void Start() {
        SetDelayTime(); // Initialize the delaytime. The delaytime is a random number between the delayrange
        StartCoroutine(Generator());

    }

    // Use coroutine to generate GameObjects in intervals. This coroutine will go on continuously
    IEnumerator Generator() {
        // Delay the start for the duration of "delay"
        yield return new WaitForSeconds(delay);

        if (active) { // If Spanwer time is active
            Transform newTransform = transform;
            GameObjectUtil.Instantiate(prefabs[0], newTransform.position);
            //GameObject.Instantiate(prefabs[0], newTransform);
            SetDelayTime();
        }
        StartCoroutine(Generator());
    }

    void Update() {
        // override the update of BasicSpawner

    }


    void SetDelayTime() {
        delay = Random.Range(delayRange.x, delayRange.y);
    }


}
