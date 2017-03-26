using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocity : MonoBehaviour {
    public Vector2 velocity = Vector2.zero;
    private Rigidbody2D body2d;

    void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
       body2d.velocity = new Vector2(velocity.x, body2d.velocity.y);
    }
}
