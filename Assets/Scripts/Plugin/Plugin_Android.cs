using UnityEngine;
using UnityEngine.UI;

namespace BiaGames.Plugins {
    public class Plugin_Android : Plugin {

        #region Native setup              

        AndroidJavaClass pluginClass;
        AndroidJavaObject instance { get { return pluginClass.GetStatic<AndroidJavaObject>("instance"); } }

        public Plugin_Android(string gameObjectName) : base(gameObjectName) { }


        override protected void PluginSetup() {
            pluginClass = new AndroidJavaClass("com.biagames.plugintest.Plugin_Test");
            pluginClass.CallStatic("setupDialog", gameObjectName);
        }

        #endregion


        #region Features

        override public void ShowAlertWithAttributes(string title, string message, string buttonTitle, string cancelButtonTitle) {
            instance.Call("showAlertWithAttributes", title, message, buttonTitle, cancelButtonTitle);
        }



        override public void DisplayText() {
            GameObject.Find("Android Test Text").GetComponent<Text>().text = pluginClass.CallStatic<string>("getMessage");
        }

        
        override public void SetText() {
            string textinput = GameObject.Find("Input Text").GetComponent<Text>().text;
            //GameObject.Find("Android Test Text").GetComponent<Text>().text = textinput;
            if (textinput != "") {
                GameObject.Find("Android Test Text").GetComponent<Text>().text = pluginClass.CallStatic<string>("setMessage", textinput);
            }
            else
                pluginClass.CallStatic("setToast", "Please Input Text");
        }


        override public void SetToast() {
            string textinput = GameObject.Find("Input Text").GetComponent<Text>().text;
            SetToast(textinput);
        }

        override public void SetToast(string text) {
            if (text != "")
                pluginClass.CallStatic("setToast", text);
            else
                pluginClass.CallStatic("setToast", "Please Input Text");
        }

        #endregion



    }
}

