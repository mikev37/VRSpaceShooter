using UnityEngine;
using System.Collections;

public class movewheel : MonoBehaviour {
	public Rigidbody rb;
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

		Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
		localVelocity.x = 0;
		localVelocity.z = 0;
		localVelocity.y = 0;
		rb.velocity = transform.TransformDirection(localVelocity);


		float torque = 2;
		if (!Input.GetKey (KeyCode.LeftControl)) {
			float Vturn = Input.GetAxis ("Vertical");
			float Hturn = Input.GetAxis("Horizontal");
			rb.AddRelativeTorque(Vector3.right * torque * Vturn);
			if (Input.GetKey (KeyCode.LeftShift)) {
				//rb.AddRelativeForce (transform.up * torque * Vturn);
				//rb.AddRelativeForce (transform.right * torque * Hturn);
				rb.AddRelativeTorque(Vector3.up * torque * Hturn);
			} else {
				rb.AddRelativeTorque(Vector3.forward * torque * Hturn);

			}

		}



	
	}
}
