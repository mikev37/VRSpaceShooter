using UnityEngine;
using System.Collections;

using Valve.VR.InteractionSystem;

public class ToyPlaneController : MonoBehaviour {


	public LinearMapping throttle;
	public JoyStickMapping control;
	public Rigidbody plane;
	public float thrustForce;
	public float turnForce;
	
	// Update is called once per frame
	void Update () {
		plane.AddForce(Vector3.up * throttle.value * thrustForce);
		plane.AddForce(transform.up * throttle.value * thrustForce);

		plane.AddRelativeTorque (control.pitch * turnForce, control.yaw * turnForce, control.rotation * turnForce);
	}
}
