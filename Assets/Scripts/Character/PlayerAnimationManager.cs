using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour {
    public float stillThreshold = 0.5f;    
    private Animator animator;
    private InputState inputState;
    private Rigidbody2D body2d;
	
	void Awake () {
        animator = GetComponent<Animator>();
        inputState = GetComponent<InputState>();
        body2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("IsRunning", inputState.leftButton || inputState.rightButton); 
        animator.SetBool("IsJumping", inputState.jumpButtonDown|| Mathf.Abs(body2d.velocity.y) > stillThreshold);
        //animator.SetBool("IsStill", Mathf.Abs(body2d.velocity.x) == 0 && Mathf.Abs(body2d.velocity.y) == 0);
    }
}
