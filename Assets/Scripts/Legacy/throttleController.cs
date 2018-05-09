using UnityEngine;
using System.Collections;

[RequireComponent( typeof( Rigidbody ) )]
public class throttleController : MonoBehaviour {
	Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKeyDown (KeyCode.A)) {
			Debug.Log ("A");
			rb.AddForce(transform.forward * 100);
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			rb.AddForce(transform.up * 100);
			Debug.Log ("D");
		}*/
		//Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
		//localVelocity.x = 0;
		//localVelocity.z = 0;
		//rb.velocity = transform.TransformDirection(localVelocity);
		if (Input.GetKey (KeyCode.LeftControl)) {
			float turn = Input.GetAxis ("Vertical");
			float torque = 2;
			rb.AddRelativeForce (Vector3.right * torque * turn);

		}
	}
}
