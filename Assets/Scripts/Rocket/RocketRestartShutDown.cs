using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketRestartShutDown : MonoBehaviour, IRecycle {
    public Vector2 velocity = Vector2.zero;   
    private Rigidbody2D body2d;
    private RocketScript rocketscript;

    void Awake() {
        body2d = GetComponent<Rigidbody2D>();
        rocketscript = GetComponentInChildren<RocketScript>();            
    }
    
    void IRecycle.Restart() {
        body2d.velocity = velocity;
        // set it to the rocket layer for every child GameObject of the rocket
        for (int i = 0; i < gameObject.transform.childCount; i++) {
            gameObject.transform.GetChild(i).gameObject.layer = 14;
        }
        rocketscript.scored = false;
    }

    void IRecycle.Shutdown() {


    }
}
