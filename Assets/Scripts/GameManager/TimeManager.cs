using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    void Update() {

    }

    public void ManipulateTime(float newTime, float duration) {
        //if (Time.timeScale == 0) Time.timeScale = 0.1f;
        StartCoroutine(FadeTo(newTime, duration));
   } 
    

    IEnumerator FadeTo(float value, float time) {   
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time) {
            float newTime = Mathf.Lerp(Time.timeScale, value, t);
            Time.timeScale = newTime;
            if (Mathf.Abs(value - Time.timeScale) <0.01) Time.timeScale= value;          
            yield return null;
        }
    }

}
