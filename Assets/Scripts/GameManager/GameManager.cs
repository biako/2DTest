using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {    
    public  List<BasicSpawner> spawners;
    private TimeManager timeManager;
    private GameObject player;    //public GameObject playerPrefab;

    public double destoryInactiveDuration = 30d; // If the recycled object is inactive for this duration, the recycled object is destoryed permanently.
    public float garbageCollectionInterval = 1f;
    public GameObject[] pools;


    private void Awake() {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }

    // Use this for initialization
    void Start() {
        ResetGame();
    }

    void Update() {
        StartCoroutine(GarbageCollection());
    }

    public void ResetGame() {
        foreach (BasicSpawner spawner in spawners) {
            spawner.active = true;
        }    
    }     

    public void ResumeGame() {
        Time.timeScale = 1;
    }

    public void PauseGame() {
        Time.timeScale = 0;
    }

    public void OnPlayerKilled() {
        foreach (BasicSpawner spawner in spawners) {
            spawner.active = false;
        }
        timeManager.ManipulateTime(0, 5f);
    }

    // Destory recycleable GameObejects being inactive for destoryInactiveDuration
    IEnumerator GarbageCollection() {
        yield return new WaitForSeconds(garbageCollectionInterval);
        {
            pools = GameObject.FindGameObjectsWithTag("ObjectPool");

            foreach (GameObject pool in pools) {

                ObjectPool poolComponent = pool.GetComponent<ObjectPool>();

                /*while (poolComponent.GetInactiveObject()) {
                    RecycleGameObject poolInstance = poolComponent.GetInactiveObject();
                    //if (DateTime.Now.Subtract(poolInstance.lastTimeDestoryed).TotalSeconds >= destoryInactiveDuration) GameObject.Destroy(poolInstance);
                }*/
                    
            }
        }
        StartCoroutine(GarbageCollection());
    }
}
