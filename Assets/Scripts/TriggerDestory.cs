using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDestory : MonoBehaviour {
    public string tagname;
    private void OnTriggerEnter2D(Collider2D collision) {
        tagname = collision.gameObject.tag;
        // If the Rocket hits the trigger...
        if (collision.gameObject.tag == "Rocket") {
            Destroy(collision.gameObject);
        }
        
    }
}
