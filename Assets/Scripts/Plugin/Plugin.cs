using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BiaGames.Plugins {
    public class Plugin {

        #region Native setup

        protected string gameObjectName;

        public static Plugin pluginWithGameObjectName(string gameObjectName) {
            Plugin plugin;

            // Get plugin class for the actual platform.
#if UNITY_IPHONE
			//plugin = (Application.isEditor)
			//	? (Plugin)new Plugin_Editor(gameObjectName)
			//	: (Plugin)new Plugin_iOS(gameObjectName);
#elif UNITY_ANDROID
            //    plugin = (Application.isEditor)
            //        ? (Plugin)new Plugin_Editor(gameObjectName)
            //        : (Plugin)new Plugin_Android(gameObjectName);
            plugin = (Plugin)new Plugin_Android(gameObjectName);
#endif

            return plugin;
        }


        public Plugin(string gameObjectName) {
            this.gameObjectName = gameObjectName;
            PluginSetup();
        }


        virtual protected void PluginSetup() { }

        #endregion


        #region Features

        virtual public void ShowAlertWithAttributes(string title, string message, string buttonTitle, string cancelButtonTitle) { }

        virtual public void DisplayText() { }

        virtual public void SetText() { }

        virtual public void SetToast() { }

        virtual public void SetToast(string text) { }

        #endregion
    }

}


