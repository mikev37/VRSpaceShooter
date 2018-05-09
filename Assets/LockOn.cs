using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Will lock on to a specific target from a list obtained from the Radar
 */
public class LockOn : MonoBehaviour {

	public WeaponsSystem system;

	public Radar radar;

	public GameObject display;

	public Transform locked;

	public Transform tracking;

	public GameObject trackingPuck;

	public Sprite trackingS,lockedS,disarmedS;

	public float lockTime = 2;

	public float timer;

	public int swapper;

	bool targetOnlyEnemy;
	bool lockToCamera = true;

	TargetPodHandler tgp;


	public void camTarget(){
		if (tgp == null)
			tracking = null;
		else
			tracking = tgp.getTarget ();


		if (tracking == null) {
			timer = lockTime;
			locked = null;
			display.SetActive (false);
			trackingPuck.SetActive (false);
		}
	}

	public void flyTarget(){
		tracking = null;
		locked = null;
		List<Transform> limitedList = radar.targetList;
		if (targetOnlyEnemy)
			limitedList.RemoveAll (item => IFFMethods.classify (item.gameObject) != IFFClass.ENEMY);

		timer = lockTime;
		if (limitedList.Count > 0) {
			swapper++;
			if (swapper >= limitedList.Count) {
				swapper = 0;
			}
			tracking = limitedList [swapper];
		} else {
			tracking = null;

		}
		display.SetActive (false);
		trackingPuck.SetActive (false);
	}

	public void switchTarget (){
		if (lockToCamera) {
			camTarget ();
		} else {
			flyTarget ();
		}
	}

	public void refreshCamera (TargetPodHandler tgp){
		this.tgp = tgp;
		if(lockToCamera)
			switchTarget ();
	}

	// Use this for initialization
	void Start () {
		display.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		lockToCamera = tgp.typeOfTrack == TrackingType.LOCK;



		bool radarTargetEscaped = !radar.targetList.Contains (tracking) && !lockToCamera;

		if (tracking == null || radarTargetEscaped) {
			switchTarget ();
		}else if (timer <= 0) {
			timer = 0;
			locked = tracking;
			display.SetActive (true);
			trackingPuck.SetActive (true);
			trackingPuck.transform.position = locked.transform.position;
			if (system.armed ()) {
				trackingPuck.GetComponentInChildren<Image> ().sprite = lockedS;
			} else {
				trackingPuck.GetComponentInChildren<Image> ().sprite = disarmedS;
			}
		} else {
			timer -= Time.deltaTime;
			trackingPuck.SetActive (true);
			trackingPuck.transform.position = tracking.transform.position;
			if (system.armed ()) {
				trackingPuck.GetComponentInChildren<Image> ().sprite = trackingS;
			} else {
				trackingPuck.GetComponentInChildren<Image> ().sprite = disarmedS;
			}
		}

		system.target = locked;

	}


}
