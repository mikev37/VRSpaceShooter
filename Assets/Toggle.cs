using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour {
	public GameObject targetObject;
	public void toggle(){
		if(targetObject != null){
			targetObject.SetActive(!targetObject.activeSelf);
		}
	}
}
