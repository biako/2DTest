using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public float jumpSpeed = 50f;
    public float forwardSpeed = 15f;

    public int jumpMinIntervalMillisecond = 300;
    private System.DateTime lastJumpTime;
    private bool canJump;

    private InputState inputState;
    private Rigidbody2D body2d;

    // Get the components during Awake()
    void Awake() {
        body2d = GetComponent<Rigidbody2D>();
        inputState = GetComponent<InputState>();
        
    }

    void Update() {
        // If standing and therere is button down, add jumpSpeed on the y of the rigidbody
        
        canJump = System.DateTime.Now.Subtract(lastJumpTime).TotalMilliseconds >= jumpMinIntervalMillisecond;

        if (inputState.standing) {
            if (inputState.jumpButton && canJump) {
                lastJumpTime = System.DateTime.Now;
                if (inputState.rightButton)
                    body2d.velocity = new Vector2(forwardSpeed, jumpSpeed);
                if (inputState.leftButton)
                    body2d.velocity = new Vector2(-forwardSpeed, jumpSpeed);
               if (!inputState.leftButton && !inputState.rightButton)
                    body2d.velocity = new Vector2(0, jumpSpeed);                
            }
            else if (inputState.rightButton) {
                body2d.velocity = new Vector2(forwardSpeed, 0);

            }
            else if (inputState.leftButton) {
                body2d.velocity = new Vector2(-forwardSpeed, 0);

            }
        }
    }
}
