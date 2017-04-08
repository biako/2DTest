using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void StartMainMenu() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void StartRcoketWar() {
        SceneManager.LoadScene("RocketWar", LoadSceneMode.Single);
    }

    public void StartAnroidPlugin() {
        // Only specifying the sceneName or sceneBuildIndex will load the scene with the Single mode
        SceneManager.LoadScene("AnroidPlugin", LoadSceneMode.Single);
    }

    public void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }

}
