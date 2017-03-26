using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<BasicSpawner> spawners;
    private TimeManager timeManager;
    
    public double destoryInactiveDuration = 30d; // If the recycled object is inactive for this duration, the recycled object is destoryed permanently.
    public float garbageCollectionInterval = 10f; // Interval for the garbagae colleciton process to run.
    public GameObject[] pools;


    private void Awake() {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }

    // Use this for initialization
    void Start() {
        ResetGame();
        StartCoroutine(GarbageCollection());
    }

    void Update() {
        
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
        timeManager.ManipulateTime(0, 0.5f);
    }

    // Destory recycleable GameObejects being inactive for destoryInactiveDuration
    IEnumerator GarbageCollection() {
        yield return new WaitForSeconds(garbageCollectionInterval);
        {
            pools = GameObject.FindGameObjectsWithTag("ObjectPool");

            foreach (GameObject pool in pools) {

                ObjectPool poolComponent = pool.GetComponent<ObjectPool>();

                foreach (RecycleGameObject poolInstance in poolComponent.pool) {
                    if (poolInstance != null) {
                        if (!poolInstance.isActiveAndEnabled &&
                            (DateTime.Now - poolInstance.lastTimeDestoryed).TotalSeconds >= destoryInactiveDuration)
                            Destroy(poolInstance.gameObject);
                    }
                }
            }
        }
        StartCoroutine(GarbageCollection());
    }
}
