using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiledBackground : MonoBehaviour {

    public static float pixelToUnit = 100f;

    public int textureSize = 256;


	// Use this for initialization
	void Start () {

        float newWidth = Mathf.Ceil(Screen.width / (textureSize * PixelPerfectCamera.scale));

        transform.localScale = new Vector3((newWidth * textureSize) / pixelToUnit, 1, 1);

        GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
