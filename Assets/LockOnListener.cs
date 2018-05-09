using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SwitchMapping))]
public class LockOnListener : MonoBehaviour {

	float resetTimer = 1f;
	float lockTimer = 0f;
	float trackTimer = 0f;

	SwitchMapping locked;
	SwitchMapping tracked;
	SwitchMapping active;

	// Use this for initialization
	void Start () {
		locked = transform.Find ("Locked").GetComponent <SwitchMapping>();
		tracked = transform.Find ("Targeted").GetComponent <SwitchMapping> ();
	}

	public static void lockOn(GameObject obj){
		if (obj.GetComponentInChildren<LockOnListener> () != null) {
			obj.GetComponentInChildren<LockOnListener> ().lockTarget ();
		}
	}

	public static void track(GameObject obj){
		if (obj.GetComponentInChildren<LockOnListener> () != null) {
			obj.GetComponentInChildren<LockOnListener> ().aquireTarget();
		}
	}

	public void aquireTarget(){
		tracked.on = true;
		trackTimer = resetTimer;
	}

	public void lockTarget(){
		locked.on = true;
		lockTimer = resetTimer;
	}
	
	// Update is called once per frame
	void Update () {
		

		if (lockTimer <= 0) {
			lockTimer = resetTimer;
			locked.on = false;
		}

		if (trackTimer <= 0) {
			trackTimer = resetTimer;
			tracked.on = false;
		}

		lockTimer -= Time.deltaTime;
		trackTimer -= Time.deltaTime;
	}
}
