using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * When close enough to a particular target, activate some event
 */
public class ActivateOnProximity : MonoBehaviour {

	public Transform target;

	public int distance;

	public UnityEvent activate;
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			if (Vector3.Distance (transform.position, target.position) < distance) {
				if (activate != null) {
					activate.Invoke ();
				}
			}
		}
	}
}
