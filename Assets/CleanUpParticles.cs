using UnityEngine;
using System.Collections;

public class CleanUpParticles : MonoBehaviour {

	public float cleanUpAfter = 3;
	
	// Update is called once per frame
	void Update () {
		cleanUpAfter -= Time.deltaTime;

		if (cleanUpAfter <= 0) {
			Destroy (this.gameObject);
		}
	
	}
}
