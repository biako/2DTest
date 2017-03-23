using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectUtil {

	public static GameObject Instantiate(GameObject prefab, Vector3 pos){
		GameObject instance = null;
		instance = GameObject.Instantiate (prefab);
		instance.transform.position = pos;
		return instance;
	}

	public static void Destroy(GameObject gameObject){
		var recyleGameObject = gameObject.GetComponent<RecycleGameObject>();

		if(recyleGameObject != null){
			recyleGameObject.Shutdown ();
		}else{
			GameObject.Destroy (gameObject);
		}
	}
}
