using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAnimationManager : MonoBehaviour {    
    private Animator animator;
    private InputState inputState;
    private Rigidbody2D body2d;       

    void Awake() {
        animator = GetComponent<Animator>();
        inputState = GetComponent<InputState>();
        body2d = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate() {
        animator.SetBool("IsJumping", !GetComponent<InputState>().standing);
#if MOBILE_INPUT
        animator.SetBool("IsRunning", inputState.rightButton || inputState.leftButton); // This is for mobile
#else
        animator.SetBool("IsRunning", Input.GetButton("Horizontal"));  // This is for PC
#endif
    }
}
