using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RecycleGameObject should be attached to the prefab that is recyclable. IRecycle should be implmeneted for other scripts that do the restart and shutdown work.
public class RecycleGameObject : MonoBehaviour {
    private List<IRecycle> recycleComponentScriptList;
    public DateTime lastTimeDestoryed; // The last time the object is destoryed.
    public double nowFromLastTimeDestoryed;

    // This is used to get all the components scripts that implements IRecycle interface of the recyclable GameObject and store the reference into a list.
    void Awake() {
        MonoBehaviour[] componentScripts = GetComponents<MonoBehaviour>();
        recycleComponentScriptList = new List<IRecycle>();
        foreach (MonoBehaviour componentScript in componentScripts) {
            if (componentScript is IRecycle) recycleComponentScriptList.Add(componentScript as IRecycle);
            // The 'as' operator is like a cast operation. However, if the conversion isn't possible, as returns null instead of raising an exception. 
        }
    }

    void Update() {
        nowFromLastTimeDestoryed = DateTime.Now.Subtract(lastTimeDestoryed).TotalSeconds;
    }

    public void Restart() {
        gameObject.SetActive(true); // Set the GameObject active

        // Call Restart in the component script
        foreach (IRecycle componentScript in recycleComponentScriptList) {
            componentScript.Restart();
        }
    }


    public void Shutdown() {
        lastTimeDestoryed = DateTime.Now;
        gameObject.SetActive(false); // Set the GameObject inactive

        // Call Shutdown in the component script
        foreach (IRecycle componentScript in recycleComponentScriptList) {
            componentScript.Shutdown();
        }
    }
}
