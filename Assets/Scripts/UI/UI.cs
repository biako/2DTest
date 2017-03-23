using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

    public static string centerText;
	
	// Update is called once per frame
	void Update () {
        
        if (centerText == "Game Over") Time.timeScale = 0;
    }
}
