using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;
public class SwitchBaseRotate : MonoBehaviour {

	public GameObject linearDrive;

	// Use this for initialization
	void Start () {
		linearDrive = this.GetComponentInParent<Indentifier> ().gameObject.GetComponentInChildren<LinearDrive> ().gameObject;
		if (linearDrive == null) {
			linearDrive = this.GetComponentInParent<Indentifier> ().gameObject.GetComponentInChildren<SwitchDrive> ().gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.LookAt (linearDrive.transform);
	}
}
