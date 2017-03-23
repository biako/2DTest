using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHurt : MonoBehaviour {
    public int health;
    // Use this for initialization

    void Awake() {

    }
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void Hurt() {

        GameObjectUtil.GameOver();

    }


}
