
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartShutdown : MonoBehaviour, IRecycle {
    // to store the sprites in an array
    public Sprite[] sprites;

    void IRecycle.Restart() {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        // Randomly select the sprite from the sprites array
        renderer.sprite = sprites[Random.Range(0, sprites.Length)];

        // Get the collider
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        // Get the size of the sprite renderer
        Vector3 size = renderer.bounds.size;

        // Set the collider's size as the size of the renderer
        collider.size = size;
        // collider.offset = new Vector2(collider.offset.x, collider.size.y / 2 - collider.offset.y);

    }

    void IRecycle.Shutdown() {
     
    }  
}
