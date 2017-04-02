using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAnimation : MonoBehaviour {
    public float point;
    private float getHitEffect;
    public float targX, targY;
    private Vector3 pointPosition;
    public Vector3 screenPos;

    public GUISkin pointSkin;
    public GUISkin pointSkinShadow;

    private bool active;

    // Update is called once per frame


    private void OnGUI() {
        if (active) {
            getHitEffect += Time.deltaTime * 30;
            GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - (getHitEffect - 50) / 7);
            //GUI.skin = pointSkinShadow;
            //GUI.Label(new Rect(targX +8 , targY - 100, 80, 70), "+" + point.ToString());
            //GUI.Label(new Rect(targX + 8, targY - 2, 80, 70), "+" + point.ToString());
            GUI.skin = pointSkin;
            
            GUI.Label(new Rect(targX, targY, 200, 200), "+" + point.ToString());
            //GUI.Label(new Rect(targX + 10, targY, 120, 120), "+" + point.ToString());
        }
    }

    void Update() {
        if (active) {
            targY -= Time.deltaTime * 200;
            if (Mathf.Abs(targY) >=1000) {
                targY = 0;
                active = false;                   
            }
        }
    }


    public void StartScoreAnimation(Vector3 pos, int points) {
        screenPos = Camera.main.GetComponent<Camera>().WorldToScreenPoint(pos);
        //screenPos = Camera.main.GetComponent<Camera>().WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position);
        targX = screenPos.x + Random.Range(10, 100);
        targY = Mathf.Abs(Screen.height - screenPos.y) - Random.Range(100, 200);
        point = points;
        active = true;
        getHitEffect = 0;
    }
}
