using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

	public bool state;
	HingeJoint joint;
	AudioSource clickSound;
	// Use this for initialization
	void Start () {
		joint = this.GetComponent<HingeJoint> ();
		clickSound = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool nState;
		if (joint.angle > 0) {
			nState = true;
			JointSpring spring = joint.spring;
			spring.targetPosition = 45;
			joint.spring = spring;
		} else {
			nState = false;
			JointSpring spring = joint.spring;
			spring.targetPosition = -45;
			joint.spring = spring;
		}

		if (nState != state) {
			clickSound.Play ();
			state = nState;
		}
	}
}
