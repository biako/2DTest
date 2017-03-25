using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour {

    public float jumpSpeed = 40f;
    public float forwardSpeed = 10f;

    private InputState inputState;
    private Rigidbody2D body2d;

    // Get the components during Awake()
    void Awake() {
        body2d = GetComponent<Rigidbody2D>();
        inputState = GetComponent<InputState>();

    }

    void Update() {

        if (inputState.standing) {
            if (inputState.jumpButtonDown) {
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
