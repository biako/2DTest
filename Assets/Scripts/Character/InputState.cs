using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputState : MonoBehaviour {
    public bool jumpButtonDown, rightButton, leftButton, noInput; // button pressed?
    public float absVelx = 0f; // the velocity of x
    public float absVely = 0f; // the velocity of y

    public bool standing; // standing?

    public float standingVelocityThreshold = 0f; // standing threshold for velocity of y
    private bool lastInputRight = true; // last input is right?

    private Rigidbody2D body2d;

    // Rigidbody should get during Awake, not Start
    void Awake() {
        body2d = GetComponent<Rigidbody2D>();

    }

    void Start() {
        lastInputRight = true;
    }

    // Input should be checked during Update
    void Update() {
        if (Time.timeScale != 0) {
            jumpButtonDown = Input.GetButtonDown("Jump");
            rightButton = Input.GetAxis("Horizontal") > 0; // Return true if right button is down.       
            leftButton = Input.GetAxis("Horizontal") < 0;
            //noInput = !(jumpButtonDown || leftButton || rightButton); // No valid input at all

            if (leftButton) {
                if (lastInputRight) flip();
                lastInputRight = false;
            }

            if (rightButton) {
                if (!lastInputRight) flip();
                lastInputRight = true;
            }
            
        }
        else {
            lastInputRight = true;
        }
    }

    // Rendering of rigidbody should be done in fixedupdate
    void FixedUpdate() {
        absVely = Mathf.Abs(body2d.velocity.y); // get the aboslute of the velocity v of the rigidbody
        //standing = (absVely <= standingVelocityThreshold); // return true if <= the threshold
        standing = (absVely == 0);
    }

    void flip() {
        if (leftButton) transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        if (rightButton) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
}
