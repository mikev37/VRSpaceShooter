using UnityEngine;
using System.Collections;

using Valve.VR.InteractionSystem;

public class LinearDriveOpenCockpit : MonoBehaviour {

	public LinearMapping openSwitch;
	public bool open;
	public int speed = 1;

	// Update is called once per frame
	void Update () {
		//Vector3 rot = transform.eulerAngles;
		Vector3 target = new Vector3 (0,70 * openSwitch.value,0);


		//float step = speed * Time.deltaTime;

		//create the rotation we need to be in to look at the target
		Quaternion _lookRotation = Quaternion.LookRotation(target);

		//Quaternion.FromToRotation (new Vector3 (0, 0, 0), target);

		//rotate us over time according to speed until we are in the required rotation
		transform.localRotation = Quaternion.Slerp(transform.localRotation,_lookRotation, Time.deltaTime * speed);



	}
}
