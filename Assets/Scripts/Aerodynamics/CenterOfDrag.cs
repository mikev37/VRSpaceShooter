using UnityEngine;
using System.Collections;
/*
 * Debug class for showing where aerodynamic forces are displayed
 */
public class CenterOfDrag : MonoBehaviour {
	// Use this for initialization
	void Update () {
		Rigidbody body = GetComponentInParent<SpaceShipCore> ().GetComponent<Rigidbody> ();

		Vector3 Velocity = body.gameObject.transform.InverseTransformDirection (body.velocity);

		Quaternion rotation = new Quaternion ();

		rotation.eulerAngles = new Vector3 (-90, 0, 0);

		Velocity = rotation * Velocity;

		this.transform.localPosition = Velocity;
	}
}
