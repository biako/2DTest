﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Actions : MonoBehaviour {

    public float jumpSpeed = 50f;
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
        if (inputState.jumpButtonDown && inputState.standing) { // When standing and jumping
            soundClips.pitch = 1.0f;
            soundClips.clip = soundClips.GetComponent<AudioClips>().jumpClip;
            soundClips.Play();
            body2d.velocity = new Vector2(body2d.velocity.x, jumpSpeed);
        }

        if ((!inputState.isCollision && !inputState.standing) || inputState.standing) { // When jumping but no obstcles and when standing

            if (inputState.rightButton) {
                body2d.velocity = new Vector2(forwardSpeed, body2d.velocity.y);
            }

            if (inputState.leftButton) {
                body2d.velocity = new Vector2(-forwardSpeed, body2d.velocity.y);
            }
        }

#if MOBILE_INPUT
        if (CrossPlatformInputManager.GetAxis("Horizontal") == 0 ) { // When no horizontal input, no horizontal velocity. This is for mobile
#else
        if (!Input.GetButton("Horizontal")) { // This is for PC
#endif
            body2d.velocity = new Vector2(0, body2d.velocity.y);
        }
    }
}
