using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public List<BasicSpawner> spawners;
    private TimeManager timeManager;
    private GameObject[] pools;
    private bool gameStarted; // Game has started?
    private bool playerKilled; // Player killed?

    public Text timeText, scoreText, continueText, gameOverText, countDownText;
    public AudioSource beepSound;
    private int blinkTime;
    private bool blink;
    public int score;
    private float timeElapsed;
    private int bestScore;


    public double destoryInactiveDuration = 30d; // If the recycled object is inactive for this duration, the recycled object is destoryed permanently.
    public float garbageCollectionInterval = 10f; // Interval for the garbagae colleciton process to run.

    private void Awake() {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }


    void Start() {
        Time.timeScale = 0;
        playerKilled = false;
        gameStarted = false;
        timeElapsed = 0;
        score = 0;
        continueText.text = "Press 'Any' Key To Continue";
        gameOverText.text = "Game Over";
        gameOverText.canvasRenderer.SetAlpha(0);
    }

    void Update() {
        if (!gameStarted && Time.timeScale == 0) {
            if (Input.anyKeyDown) {
                ResumeGame();
                ResetGame();
            }

        }

        if (Input.GetKeyDown(KeyCode.P) && gameStarted) {
            if (Time.timeScale == 1) {
                PauseGame();
            }
            else if (Time.timeScale == 0) {
                ResumeGame();
            }
        }


        if (!gameStarted) {
            blinkTime++;
            if (blinkTime % 40 == 0) {
                blink = !blink;
            }
            continueText.canvasRenderer.SetAlpha(blink ? 0 : 1);
        }
        else {
            timeElapsed += Time.deltaTime;
            timeText.text = FormatTime(timeElapsed);
            scoreText.text = score.ToString();
            continueText.canvasRenderer.SetAlpha(0);
            gameOverText.canvasRenderer.SetAlpha(0);
        }
    }



    string FormatTime(float value) {
        TimeSpan t = TimeSpan.FromSeconds(value);
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }


    public void ResetGame() {
        if (playerKilled) {
            GameObjectUtil.Destroy(GameObject.FindGameObjectsWithTag("Player"));
            GameObjectUtil.Destroy(GameObject.FindGameObjectsWithTag("Rocket"));
        }
        gameStarted = true;
        StartCoroutine(CountDownResetGame(3));
    }

    private IEnumerator CountDownResetGame(int seconds) {
        countDownText.canvasRenderer.SetAlpha(1);
        beepSound.pitch = 0.8f;
        int i = seconds;
        while (i > 0) {
            beepSound.Play();
            countDownText.text = i.ToString();
            i--;
            yield return new WaitForSeconds(1);
        }
        beepSound.Play();
        beepSound.pitch = 1.6f;
        countDownText.text = "Go!";
        yield return new WaitForSeconds(0.5f);
        countDownText.canvasRenderer.SetAlpha(0);


        foreach (BasicSpawner spawner in spawners) {
            spawner.active = true;
        }
        timeElapsed = 0;
        StartCoroutine(GarbageCollection());
    }


    void ResumeGame() {
        Time.timeScale = 1;
    }

    void PauseGame() {
        Time.timeScale = 0;
    }

    public void OnPlayerKilled() {
        foreach (BasicSpawner spawner in spawners) {
            spawner.active = false;
        }
        Time.timeScale = 0;
        //timeManager.ManipulateTime(0, 0.5f);
        gameStarted = false;
        playerKilled = true;
        continueText.text = String.Format("Press 'Any' Key To Try Again");
        gameOverText.canvasRenderer.SetAlpha(1);
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
