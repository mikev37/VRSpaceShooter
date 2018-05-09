using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingClamp : MonoBehaviour {

	public bool proximity;
	public float mindistance = .5f;

	public bool alignment;
	public float minmisalignment = 5;

	public bool relVelocity;
	public float minRelVelocity = 1;

	public bool relAngle;
	public float minRelAngle = 1;

	public GameObject target;

	public Transform forward;

	public AudioClip dockSound, unDockSound;


	AudioSource audio;

	GameObject spaceShip;

	public void UnDock(){
		Vector3 push = forward.transform.position - transform.position;

		if (target != null) {
			
			Rigidbody rb = target.GetComponentInParent<Rigidbody> ();

			rb.AddForce ( push, ForceMode.Impulse);

			target.GetComponent<DockingClamp> ().target = null;
			target = null;
		}


		spaceShip.GetComponent<Rigidbody> ().AddForce (-1 * push, ForceMode.Impulse);

		Component.Destroy (spaceShip.GetComponent<FixedJoint> ());

		audio.clip = unDockSound;
		audio.Play ();
	}

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		spaceShip = GetComponentInParent<SpaceShipCore> ().gameObject;
	}

	public void setClampTarget(GameObject input){
		if(target == null){
			target = input.GetComponentInChildren<DockingClamp> ().gameObject;
			target.GetComponent<DockingClamp> ().setClampTarget (gameObject);
		}
	}

	public void Dock(){
		
		//check validity
		if (!proximity)
			return;
		if (!alignment)
			return;
		//check validity
		if (!relVelocity)
			return;
		if (!relAngle)
			return;

		Transform targetShip = target.GetComponentInParent<Rigidbody> ().transform;
		//force proximity
		Vector3 positionOffset = transform.position - target.transform.position;

		targetShip.position += positionOffset;
	
		//force alignment

		//deemed difficult and unnecessary

		//create a lock

		FixedJoint joint = spaceShip.AddComponent<FixedJoint> ();

		joint.connectedBody = targetShip.GetComponent<Rigidbody> ();

		//play noise

		audio.clip = dockSound;
		audio.Play ();

		//play animation

	}

	public Quaternion forceAlign(){
		Vector3 foward = forward.position - transform.position;
		Vector3 toward = target.transform.position - transform.position;


		return Quaternion.FromToRotation(foward,toward);

	}

	public float alignmentDifference(){
		Vector3 foward = forward.position - transform.position;
		Vector3 toward = target.transform.position - transform.position;

		return Vector3.Angle (foward, toward);
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			proximity = Vector3.Distance (transform.position, target.transform.position) < mindistance;

			alignment = alignmentDifference () <= minmisalignment &&
				target.GetComponent<DockingClamp> ().alignmentDifference ()
				<= target.GetComponent<DockingClamp> ().minmisalignment;

			Rigidbody mrb = GetComponentInParent<Rigidbody> ();

			Rigidbody trb = target.GetComponentInParent<Rigidbody> ();


			relVelocity = (mrb.velocity - trb.velocity).magnitude < minRelVelocity;

			relAngle = (mrb.angularVelocity - trb.angularVelocity).magnitude < minRelAngle;


			if (spaceShip != null) {
				if (Input.GetKeyDown (KeyCode.Space)) {
					Dock ();
				}

				if (Input.GetKeyDown (KeyCode.LeftAlt)) {
					UnDock ();
				}
			}
		
		}
			
	}
}
