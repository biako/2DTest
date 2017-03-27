using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketRestartShutDown : MonoBehaviour, IRecycle {
    public Vector2 velocity = Vector2.zero;   
    private Rigidbody2D body2d;

    void Awake() {
        body2d = GetComponent<Rigidbody2D>();
    }
    
    void IRecycle.Restart() {
        body2d.velocity = velocity;
        gameObject.layer = 14; // set it to the rocket layer

    }

    void IRecycle.Shutdown() {


    }
}
