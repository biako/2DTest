using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputState : MonoBehaviour {
    private Rigidbody2D body2d;
    private BoxCollider2D collider2d; // the collider2d component from the GameObject that the script is attached to. ATTENTION: must be BoxCollider2D, because its base class Collider2D does not have the variable .size.
    private RaycastHit2D hitBottomLeft; // the RaycastHit2D of the left side of the collider to detect if grounded.
    private RaycastHit2D hitBottomRight; // the RaycastHit2D of the right side of the collider to detect if grounded.
    private float hitBottomDistanceLeft; // the distance from left side of the collider to the ground
    private float hitBottomDistanceRight; // the distance from right side of the collider to the ground
    public float standingGroundThreshold = 0.5f; // the threshold for detecting if grounded. if hitDistance is less than the threshold, the GameObject is determined to be grounded.
    public bool standing; // is standing on ground?

    public bool isCollision;    

    public bool lastInputRight = true; // last input is right?

    public bool jumpButtonDown, rightButton, leftButton; // which button pressed? 
   

    // Rigidbody should get during Awake, not Start
    void Awake() {
        body2d = GetComponent<Rigidbody2D>();
        lastInputRight = true; // to reset the bool to set facing right initially

    }

    void Start() {
        lastInputRight = true; // to reset the bool to set facing right initially
    }

    // Input should be checked during Update
    void Update() {
        if (Time.timeScale != 0) {
            jumpButtonDown = Input.GetButtonDown("Jump");
            rightButton = Input.GetAxis("Horizontal") > 0; // Return true if right button is down.       
            leftButton = Input.GetAxis("Horizontal") < 0; // Return true if left button is down.       

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

    // Rendering in connection with rigidbody should be done in FixedUpdate instead of Update
    void FixedUpdate() {
        collider2d = GetComponent<BoxCollider2D>(); // get the BoxCollider2D component from the GameObject
        standing = IsStanding(collider2d);
    }

    bool IsStanding(BoxCollider2D collider2d) {
        // cast the ray from the left bottom and right bottom edge of the BoxCollider2D downward. (Vector2.down == new Vector2 (0,-1))
        hitBottomLeft = Physics2D.Raycast(new Vector2(transform.position.x - collider2d.size.x / 2 + collider2d.offset.x, transform.position.y - collider2d.size.y / 2 + collider2d.offset.y), Vector2.down);
        hitBottomRight = Physics2D.Raycast(new Vector2(transform.position.x + collider2d.size.x / 2 + collider2d.offset.x, transform.position.y - collider2d.size.y / 2 + collider2d.offset.y), Vector2.down);


        // if the ray from the left bottom or right bottom hit a collider with tag "ground", get the hitDistanceLeft and hitDistanceRight
        if (hitBottomRight.collider.tag == "ground") hitBottomDistanceRight = hitBottomRight.distance;
        if (hitBottomLeft.collider.tag == "ground") hitBottomDistanceLeft = hitBottomLeft.distance;

        return (hitBottomDistanceLeft <= standingGroundThreshold) || (hitBottomDistanceRight <= standingGroundThreshold); // return true (the GameObejct is grounded) if hitDistance from either side is less than or equal to the threshold. (if either side is grounded, it is grounded) 
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        isCollision = true;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        isCollision = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        isCollision = false;
    } 

    
    void flip() {
        if (leftButton) transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        if (rightButton) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
}
