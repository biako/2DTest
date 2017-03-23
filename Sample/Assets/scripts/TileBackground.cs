using UnityEngine;
using System.Collections;

public class TileBackground : MonoBehaviour {

	public static float pixelToUnit = 100f;
	public int textureSize = 128;

	void Start () {
		var newWidth = Mathf.Ceil (Screen.width/(textureSize * PiexelPerfectCamera.scale));
		transform.localScale= new Vector3 ((newWidth*textureSize)/pixelToUnit,1,1);
		GetComponent<Renderer>().material.mainTextureScale = new Vector3 (newWidth,1,1);
	}
}
