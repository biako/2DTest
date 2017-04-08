using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BiaGames.Plugins;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {

    #region User hooks

    [System.Serializable]
    public class DialogBoxText {
        public string title;
        public string message;
        public string buttonTitle = "OK";
        public string cancelButtonTitle = "Cancel";
    }
    public DialogBoxText text;

    [System.Serializable]
    public class StringEvent : UnityEvent<string> { }    

    // ...

    #endregion


    #region Creation

    private Plugin plugin;

    void Start() {
        // Create plugin instance (of whichever platform).
        plugin = Plugin.pluginWithGameObjectName(gameObject.name);
    }

    #endregion


    #region Features

    public void Show() {
        // Call plugin (of whichever platform).
        plugin.ShowAlertWithAttributes(
            text.title,
            text.message,
            text.buttonTitle,
            text.cancelButtonTitle
        );
    }

    // ...

    #endregion


    #region Plugin callbacks (from whichever platform)

    // TODO: Make it mandatory with an interface.
    public void AlertDidFinishWithResult(string selectedButtonTitle) {
        // Call event hooked in.
        GameObject.Find("Android Test Text").GetComponent<Text>().text = selectedButtonTitle + " is pressed!";
    }
    #endregion


    public void DisplayText() {
        plugin.DisplayText();
    }

    public void SetText() {
        plugin.SetText();
    }

    public void SetToast() {
        plugin.SetToast();
    }



}
