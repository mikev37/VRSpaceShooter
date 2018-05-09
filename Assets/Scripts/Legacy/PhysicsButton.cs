using UnityEngine;
using System.Collections;

public class PhysicsButton : MonoBehaviour {

	public bool state;
	public bool reset = true;
	ConfigurableJoint joint;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		joint = this.GetComponent<ConfigurableJoint> ();
		audio = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 distance = joint.connectedBody.transform.position - this.transform.position;
		if (reset) {
			if (distance.magnitude < 0.01) {
				state = !state;
				audio.Play ();
				reset = false;
			}
		} else {
			if (distance.magnitude > 0.01) {
				reset = true;
			}
		}

		Debug.Log (state);

		if (state) {
			joint.targetPosition = new Vector3(0,-.0f,0);
		} else {
			joint.targetPosition = new Vector3(0,-2f,0);
		}
	}
}
