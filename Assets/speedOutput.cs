using UnityEngine;
using System.Collections;
/*
 * Debug class for showing an object velocity
 */
public class speedOutput : MonoBehaviour {

	public float speed;
	
	// Update is called once per frame
	void Update () {
		speed = GetComponent<Rigidbody> ().velocity.magnitude;
	}
}
