using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOffScreen : MonoBehaviour {
    public float destoryOffset = 3f;
    private bool offscreen;
    private float offscreenX = 0, offscreenY = 0;
    private Rigidbody2D body2d;

    void Awake() {
        body2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start() {
        offscreenX = (Screen.width / 100) / 2 + destoryOffset; // offScreenX is the position of the offscreen boundary. 100 is the pixel to unit value. (PixelPerfectCamera.pixeltoUnit in the PixelPerfectCamera script)
        offscreenY = (Screen.height / 100) / 2 + destoryOffset;

    }

    // Update is called once per frame
    void Update() {
        float dirX = body2d.velocity.x; // dirX is the velocity of the GameObject
        float posX = transform.position.x; // posX is the x of the transform of the GameObject
        float dirY = body2d.velocity.y; // dirY is the velocity of the GameObject
        float posY = transform.position.y; // posY is the y of the transform of the GameObject

        if (Mathf.Abs(posX) > offscreenX || Mathf.Abs(posY) > offscreenY) {
            if (dirX < 0 && posX < -offscreenX) { // if facing left
                offscreen = true;
            }
            else if (dirX > 0 && posX > offscreenX) { // if facing right
                offscreen = true;
            }
            else if (dirX < 0 && posY < -offscreenY) { // if facing down
                offscreen = true;
            }
            else {
                offscreen = false;
            }
        }

        if (offscreen) {
            OnOutOfBounds();
        }
    }

    void OnOutOfBounds() {
        offscreen = false;
        GameObjectUtil.Destroy(gameObject);        
    }
}


