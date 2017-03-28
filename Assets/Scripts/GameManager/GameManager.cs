﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public List<BasicSpawner> spawners;
    //private TimeManager timeManager;
    private GameObject[] pools;
    private bool gameStarted; // Game has started?
    private bool playerKilled; // Player killed?
    private bool firstTime; // Is this the first time for the game to start? If first time, use any key to continue; or use enter key to try again

    public Text timeText, scoreText, continueText, gameOverText, countDownText;
    public AudioSource soundClips, musicClips;

    private bool gameOver;
    private int blinkTime;
    private bool blink;
    public int score;
    private int bestScore;
    public int totalSeconds;
    public float timeRemaining;


    public double destoryInactiveDuration = 30d; // If the recycled object is inactive for this duration, the recycled object is destoryed permanently.
    public float garbageCollectionInterval = 10f; // Interval for the garbagae colleciton process to run.

    private void Awake() {
        //timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }


    void Start() {
        Time.timeScale = 0;
        playerKilled = false;
        gameStarted = false;
        firstTime = true;
        score = 0;
        continueText.text = "Press 'Any' Key To Continue";
        scoreText.text = "0";
        gameOverText.text = "Game Over";
        gameOverText.canvasRenderer.SetAlpha(0);
        totalSeconds = 60;
        timeRemaining = totalSeconds;
        timeText.text = FormatTime(timeRemaining);
    }

    void Update() {
        // if game has not started, press key to start
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (!gameStarted && Time.timeScale == 0) {
            if ((firstTime && Input.anyKeyDown)
                || (!firstTime && Input.GetKeyDown(KeyCode.Return)
                )) {
                firstTime = false;
                ResumeGame();
                ResetGame();
            }
        }

        // if game has started, for the pauser:
        if (Input.GetKeyDown(KeyCode.P) && gameStarted && !gameOver) {
            soundClips.pitch = 1.0f;
            soundClips.clip = soundClips.GetComponent<AudioClips>().startClip;
            soundClips.Play();
            if (Time.timeScale == 1) {
                PauseGame();
            }
            else if (Time.timeScale == 0) {
                ResumeGame();
            }
        }

        // if game has not started, set the blink text
        if (!gameStarted) {
            blinkTime++;
            if (blinkTime % 30 == 0) {
                blink = !blink;
            }
            continueText.canvasRenderer.SetAlpha(blink ? 0 : 1);
        }
        // if game has started, set the timer text
        else {
            scoreText.text = score.ToString();
            continueText.canvasRenderer.SetAlpha(0);
            if (!gameOver) { // time is not up, count down time
                if (timeRemaining >= 0) {
                    timeRemaining -= Time.deltaTime;
                    timeText.text = FormatTime(timeRemaining);
                }

                else { // time is up
                    timeRemaining = 0;
                    timeText.text = FormatTime(timeRemaining);
                    gameOverText.text = "You Win!";
                    OnGameEnd(false);
                }
            };
        }
    }



    string FormatTime(float value) { // convert to time format as "00:00"
        TimeSpan t = TimeSpan.FromSeconds(value);
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }


    public void ResetGame() {

        if (playerKilled) { // if player has been killed, destory the player and rockets
            GameObjectUtil.Destroy(GameObject.FindGameObjectsWithTag("Player"));
            GameObjectUtil.Destroy(GameObject.FindGameObjectsWithTag("Rocket"));
        }

        gameOver = false;
        continueText.text = "";
        scoreText.text = "0";
        gameOverText.text = "Game Over";
        gameOverText.canvasRenderer.SetAlpha(0);
        countDownText.canvasRenderer.SetAlpha(1);
        timeRemaining = totalSeconds;
        timeText.text = FormatTime(timeRemaining);
        score = 0;
        timeText.text = FormatTime(0);
        StartCoroutine(CountDownResetGame(3));
    }

    private IEnumerator CountDownResetGame(int seconds) {
        countDownText.text = "";
        soundClips.pitch = 1.0f;
        soundClips.clip = soundClips.GetComponent<AudioClips>().startClip;
        soundClips.Play();
        timeRemaining = totalSeconds;
        timeText.text = FormatTime(timeRemaining);
        yield return new WaitForSeconds(1);

        soundClips.pitch = 0.8f;
        soundClips.clip = soundClips.GetComponent<AudioClips>().beepClip;
        int i = seconds;
        while (i > 0) {
            soundClips.Play();
            countDownText.text = i.ToString();
            i--;
            yield return new WaitForSeconds(1);
        }

        soundClips.pitch = 1.6f;
        soundClips.clip = soundClips.GetComponent<AudioClips>().beepClip;
        soundClips.Play();
        countDownText.text = "Go!";

        yield return new WaitForSeconds(0.5f);
        countDownText.canvasRenderer.SetAlpha(0);

        gameStarted = true;

        musicClips.clip = musicClips.GetComponent<Music>().musicMusic;
        musicClips.loop = true;
        musicClips.Play();

        foreach (BasicSpawner spawner in spawners) { // set all the spawners active
            spawner.active = true;
        }

        StartCoroutine(GarbageCollection()); // start garbage collection coroutine

    }


    void ResumeGame() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) player.GetComponent<InputState>().enabled = true; // enable the input
        Time.timeScale = 1;
    }

    void PauseGame() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) player.GetComponent<InputState>().enabled = false; // disable the input
        Time.timeScale = 0;
    }



    public void OnGameEnd(bool killed) {
        if (!gameOver) {
            gameOver = true;
            foreach (BasicSpawner spawner in spawners) {  // set all the spawners inactive
                spawner.active = false;
            }
            StartCoroutine(GameEnd(killed));
        }
    }

    IEnumerator GameEnd(bool killed) {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<InputState>().enabled = false;
        player.GetComponent<Actions>().enabled = false;
        player.GetComponent<Animator>().enabled = false;
        musicClips.loop = false;
        player.layer = 15; // set player to Gameover1 layer. no collision with rockets any more.

        if (killed) {
            musicClips.clip = musicClips.GetComponent<Music>().deathMusic1;
            musicClips.Play();
            yield return new WaitForSeconds(1);
            player.layer = 16; // set player to Gameover2 layer. the player will fall

            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10f);
            musicClips.clip = musicClips.GetComponent<Music>().deathMusic2;
            musicClips.Play();
            yield return new WaitForSeconds(3);

            playerKilled = true;
            gameOverText.canvasRenderer.SetAlpha(1);
            musicClips.clip = musicClips.GetComponent<Music>().gameOverMusic;
            musicClips.Play();
            yield return new WaitForSeconds(5);
        }

        else {
            playerKilled = true;
            gameOverText.canvasRenderer.SetAlpha(1);
            musicClips.clip = musicClips.GetComponent<Music>().timeupMusic; //
            musicClips.Play();
            yield return new WaitForSeconds(7);
        }

        gameStarted = false;
        continueText.text = String.Format("Press Enter Key To Try Again");
        Time.timeScale = 0;
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
