using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * Display to a Spherical plane.
 */
public class Radar3DDisplay : TrackingRadar {

	public float range;

	public override void positionTracker(Transform tracker, Transform actual){
		Vector3 relativePosition = radar.transform.InverseTransformPoint (actual.position);
		tracker.localPosition =relativePosition;
		tracker.localPosition /= range;
		tracker.rotation = Quaternion.LookRotation (tracker.position - Camera.main.transform.position);
	}

}
