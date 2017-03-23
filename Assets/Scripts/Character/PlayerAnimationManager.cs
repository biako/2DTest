using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour {
    public float StillThreshold = 0.01f;    
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
        animator.SetBool("IsRunning", Mathf.Abs(body2d.velocity.x)>0);
        animator.SetBool("IsStill", Mathf.Abs(body2d.velocity.x) < StillThreshold && Mathf.Abs(body2d.velocity.y) < StillThreshold);
        animator.SetBool("IsJumping", Mathf.Abs(body2d.velocity.y) > StillThreshold);
    }
}
