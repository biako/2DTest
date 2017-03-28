using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour {
    private float bkWidth;
    private float bkHeight;
    private GameObject maincamera, player;
    public float percentMove = .05f;

    // Use this for initialization
    private void Awake() {        
        maincamera = GameObject.Find("Main Camera");
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            if (maincamera.transform.position.x >= 0) {
                gameObject.transform.position = new Vector3(maincamera.transform.position.x + (player.transform.position.x * percentMove), gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else {
                gameObject.transform.position = new Vector3(maincamera.transform.position.x + (player.transform.position.x * percentMove), gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
    }
}