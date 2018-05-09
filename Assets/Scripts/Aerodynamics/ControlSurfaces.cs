using UnityEngine;
using System.Collections;
/*
 * This will turn a ship based off the atmospheric pressure
 */
[RequireComponent(typeof( TrilinearMapping))]
public class ControlSurfaces : MonoBehaviour {
	
	public TrilinearMapping controlInput;
	public Rigidbody body;
	public Environment env;
	[Tooltip("Pitch, Yaw, Roll")]public Vector3 baseTurnForce;
	// Use this for initialization
	void Start () {
		controlInput = GetComponent<TrilinearMapping> ();
		body = GetComponentInParent<SpaceShipCore> ().GetComponent<Rigidbody> ();
		env = GetComponentInParent<SpaceShipCore> ().environment;
	}

	void Update(){
		//TODO Add rotation based off local z angle and control input

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float turnForce = env.atmosphericDensity * body.velocity.sqrMagnitude;
		body.AddRelativeTorque (controlInput.value.x * turnForce * baseTurnForce.x, controlInput.value.y * turnForce * baseTurnForce.y, controlInput.value.z * turnForce * baseTurnForce.z);
	}
}
