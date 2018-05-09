using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 * This is the Radar used by lock on systems and the HUD
 * 
 * It tracks objects within a trigger boudnings box
 */
public class Radar : MonoBehaviour {

	public float minSize = 1;
	public float minRange = 1;
	public List<Transform> targetList;
	// Use this for initialization
	void Start () {
		targetList = new List<Transform> ();
	}

	void Update () {
		targetList.RemoveAll(item => item == null);
	}

	void OnTriggerEnter(Collider other) {
		if(other.attachedRigidbody != null && other.attachedRigidbody.mass > minSize)
			targetList.Add (other.attachedRigidbody.transform);
	}

	void OnTriggerExit(Collider other) {
		targetList.Remove (other.attachedRigidbody.transform);
	}


}
