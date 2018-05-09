using UnityEngine;
using System.Collections;

public class TargetLocked : MonoBehaviour {

	public Transform target;

	public float accuracy = .5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			if (GetComponent<ActivateOnProximity> () != null) {
				GetComponent<ActivateOnProximity> ().target = target;
			}

			LockOnListener.lockOn (target.gameObject);

			if (target.gameObject.GetComponent<Rigidbody>() != null) {
				float speed = 100;
				if (GetComponent<Rigidbody> () != null) {
					speed = GetComponent<Rigidbody> ().velocity.magnitude;
				}
				float timeToTarget = Vector3.Distance (transform.position, target.transform.position) / speed;
				Vector3 prediction = (target.gameObject.GetComponent<Rigidbody>().position + target.gameObject.GetComponent<Rigidbody>().velocity * timeToTarget);
				transform.rotation = (Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (prediction - transform.position), accuracy));
			} else {
				transform.rotation = (Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (target.position - transform.position), accuracy));
			}
		}
	}
}
