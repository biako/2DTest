using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScore : MonoBehaviour {
    public GameManager gameManager;
    public AudioSource soundClips;
    public string coltag;
    // Use this for initialization
    void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        soundClips = GameObject.Find("SoundClips1").GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision) {    
        coltag = collision.gameObject.tag;  
        
        if (collision.gameObject.tag == "Player") {
            gameObject.transform.parent.gameObject.layer = 16; // set it to the Gameover2 layer, so that no interaction with other objects
            soundClips.pitch = 1.0f;
            soundClips.clip = soundClips.GetComponent<AudioClips>().scoreClip;
            soundClips.Play();
            gameManager.score += 10; // add score
            if (gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
                gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -30f); // push the rocket down
            else gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 30f); // push the rocket up
            

        }

    }
}
