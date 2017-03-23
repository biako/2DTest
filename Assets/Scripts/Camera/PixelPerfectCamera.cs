using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour {

    public static float pixeltoUnit = 100f;
    public static float scale = 1f;
    public float screenHeight;
    public Vector2 nativeResolution = new Vector2(1920, 1080);

    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        if(camera.orthographic)
        {
            scale = Screen.height / nativeResolution.y;
            screenHeight = Screen.height;
            pixeltoUnit *= scale;
            camera.orthographicSize = (Screen.height / 2) / pixeltoUnit;
        }

    }

}
