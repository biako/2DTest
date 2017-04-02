using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScore : MonoBehaviour {
    public GameManager gameManager;
    public AudioSource soundClips;
    public string coltag;
    public ScoreAnimation scoreAnimation;

    private Vector3 priorPosition;

    // Use this for initialization
    void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        soundClips = GameObject.Find("SoundClips1").GetComponent<AudioSource>();
        scoreAnimation = gameObject.transform.parent.GetComponent<ScoreAnimation>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        priorPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        coltag = collision.gameObject.tag;

        if (collision.gameObject.tag == "Player") {
            // set it to the Gameover2 layer for every child GameObject of the rocket, so that no interaction with other objects   
            for (int i=0; i< gameObject.transform.parent.childCount; i++) {
                gameObject.transform.parent.GetChild(i).gameObject.layer = 16;
            }

            soundClips.pitch = 1.0f;
            soundClips.clip = soundClips.GetComponent<AudioClips>().scoreClip;
            soundClips.Play();

            gameManager.score += 10; // if player hits rockt, add score
            scoreAnimation.StartScoreAnimation(priorPosition, 10);

            gameObject.transform.parent.GetComponentInChildren<RocketScript>().scored = true;
            if (gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
                gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -30f); // push the rocket down
            else gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 30f); // push the rocket up

        }

    }
}




