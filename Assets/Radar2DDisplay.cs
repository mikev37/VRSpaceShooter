using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * Display to a flat 2D plane.
 */
public class Radar2DDisplay : TrackingRadar {

	public float range;
	float scale;
	void Start(){
		base.Start();
		scale = ((RectTransform)GetComponentInParent<Canvas> ().transform).rect.width;
	}

	public override void positionTracker(Transform tracker, Transform actual){
		Vector3 xy = Vector3.up + Vector3.right;
		Vector3 relativePosition = radar.transform.InverseTransformPoint (actual.position);
		tracker.localPosition = Vector3.Scale(relativePosition,xy);

		tracker.localPosition /= range;
		tracker.localPosition *= scale;
		tracker.localScale = Vector3.one*scale;
		Text textfield = tracker.GetComponentInChildren<Text> ();
		textfield.text = (relativePosition.z).ToString();
	}

}
