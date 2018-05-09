using UnityEngine;
using System.Collections;

public class TurnOffAfterTime : MonoBehaviour {

	public GameObject[] turnOffGameObjects;
	public Behaviour[] turnOffScripts;
	public float cleanUpAfter = 3;

	// Update is called once per frame
	void Update () {
		cleanUpAfter -= Time.deltaTime;

		if (cleanUpAfter <= 0) {
			foreach (GameObject go in turnOffGameObjects) {
				go.SetActive (false);
			}
			foreach (Behaviour mo in turnOffScripts) {
				mo.enabled = false;
			}
			if (GetComponent<Rigidbody> () != null) {
				GetComponent<Rigidbody> ().drag = 0;
			}
		}

	}
}
