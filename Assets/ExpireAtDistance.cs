using UnityEngine;
using System.Collections;

public class ExpireAtDistance : MonoBehaviour {

	Vector3 recording;
	// Use this for initialization
	void Awake () {
		recording = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, recording) > 50000) {
			Destroy (gameObject);	
		}
	}
}
