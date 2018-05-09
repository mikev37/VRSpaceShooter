using UnityEngine;
using System.Collections;

using Valve.VR.InteractionSystem;

public class AfterBurner : MonoBehaviour {

	public LinearMapping amount;

	public int thrust;

	public Rigidbody body;
	
	// Update is called once per frame
	void Update () {
		body.AddForce (this.transform.forward * thrust * amount.value);
	}
}
