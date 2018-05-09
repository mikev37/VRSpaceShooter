using UnityEngine;
using System.Collections;
/*
 * Attach this to an object to easily change the CoM
 */
public class CenterOfMass : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GetComponentInParent<SpaceShipCore> ().GetComponent<Rigidbody> ().centerOfMass = transform.localPosition;
	}
		
}
