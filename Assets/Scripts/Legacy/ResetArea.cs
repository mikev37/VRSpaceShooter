using UnityEngine;
using System.Collections;

public class ResetArea : MonoBehaviour {

	public GameObject head;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		SteamVR_TrackedController control = GetComponentsInChildren<SteamVR_TrackedController> () [0];
		if (control.gripped) {
			this.transform.position = this.transform.position - head.transform.position;
			this.transform.Rotate (-1 * head.transform.rotation.eulerAngles);
		}
	}
}
