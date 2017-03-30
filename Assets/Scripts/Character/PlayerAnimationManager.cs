using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        animator.SetBool("IsRunning", Input.GetButton("Horizontal"));       
    }
}
