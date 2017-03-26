using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour {

    public float jumpSpeed = 40f;
    public float forwardSpeed = 10f;
    public AudioSource soundClips;

    private InputState inputState;
    private Rigidbody2D body2d;

    // Get the components during Awake()
    void Awake() {
        body2d = GetComponent<Rigidbody2D>();
        inputState = GetComponent<InputState>();
        soundClips = GameObject.Find("SoundClips1").GetComponent<AudioSource>();
    }

    void Update() {

        if (inputState.standing) {
            if (inputState.jumpButtonDown) {
                soundClips.pitch = 1.0f;
                soundClips.clip = soundClips.GetComponent<AudioClips>().jumpClip;
                soundClips.Play();
                if (inputState.rightButton)
                    body2d.velocity = new Vector2(forwardSpeed, jumpSpeed);
                if (inputState.leftButton)
                    body2d.velocity = new Vector2(-forwardSpeed, jumpSpeed);
                if (!inputState.leftButton && !inputState.rightButton)
                    body2d.velocity = new Vector2(body2d.velocity.x, jumpSpeed); //body2d.velocity.x?
            }
            else if (inputState.rightButton) {
                if (inputState.jumpButtonDown)
                    body2d.velocity = new Vector2(forwardSpeed, jumpSpeed);
                else body2d.velocity = new Vector2(forwardSpeed, 0);

            }
            else if (inputState.leftButton) {
                if (inputState.jumpButtonDown)
                    body2d.velocity = new Vector2(-forwardSpeed, jumpSpeed);
                else body2d.velocity = new Vector2(-forwardSpeed, 0);

            }
        }
    }


}
