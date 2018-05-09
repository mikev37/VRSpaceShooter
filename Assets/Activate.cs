using UnityEngine;
using System.Collections;

/*
 * Opposite of TurnOffAfterTime, this will turn on a series of objects and scripts after a time perioud.
 */
public class Activate : MonoBehaviour {

	public GameObject[] turnOnGameObjects;
	public Behaviour[] turnOnScripts;
	public float startUpAfter = 1;

	// Update is called once per frame
	void Update () {
		startUpAfter -= Time.deltaTime;

		if (startUpAfter <= 0) {
			foreach (GameObject go in turnOnGameObjects) {
				go.SetActive (true);
			}
			foreach (Behaviour mo in turnOnScripts) {
				mo.enabled = true;
			}
			this.enabled = false;
		}

	}
}
