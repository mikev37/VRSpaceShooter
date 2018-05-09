using UnityEngine;
using System.Collections;

public class Launch : MonoBehaviour {


	public float charge = 1000;
	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody> ().AddForce (this.transform.forward * charge,ForceMode.Impulse);
	}
}
