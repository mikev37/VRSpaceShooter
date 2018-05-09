using UnityEngine;
using System.Collections;
/*
 * Container for the aerodynamic effects of a plane
 */
public class Wing : MonoBehaviour {
	[Tooltip ("The drag produced by the wing in the x, y and z direction")]public Vector3 drag;
	[Tooltip ("The lift produced by the wing in the x, y and z direction")]public Vector3 lift;

	public float getDrag(Rigidbody body){
		Vector3 velocity = body.gameObject.transform.InverseTransformDirection (body.velocity.normalized);//body.velocity;
		return Mathf.Abs(Vector3.Dot(drag,velocity));
	}

	public float getLift(Rigidbody body){
		Vector3 velocity = body.gameObject.transform.InverseTransformDirection (body.velocity.normalized);//body.velocity;
		return Mathf.Abs(Vector3.Dot(lift,velocity));
	}

	// Update is called once per frame
	void Update () {
	
	}
}
