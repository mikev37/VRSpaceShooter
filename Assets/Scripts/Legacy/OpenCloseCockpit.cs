using UnityEngine;
using System.Collections;

public class OpenCloseCockpit : MonoBehaviour {

	public Switch openSwitch;
	public bool open;
	public int speed = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 rot = transform.eulerAngles;
		Vector3 target = new Vector3 (0,0,0);
		if (open) {
			//target.Set (0, -10, 30);
			target.Set (00, 70, 0);
		}
			
		//float step = speed * Time.deltaTime;

		//create the rotation we need to be in to look at the target
		Quaternion _lookRotation = Quaternion.LookRotation(target);

		//Quaternion.FromToRotation (new Vector3 (0, 0, 0), target);

		//rotate us over time according to speed until we are in the required rotation
		transform.localRotation = Quaternion.Slerp(transform.localRotation,_lookRotation, Time.deltaTime * speed);
			



		//transform.LookAt (target);
		//transform.position = Vector3.MoveTowards(transform.position, target, step);
		//transform.localEulerAngles = (Vector3.RotateTowards(transform.rotation.eulerAngles,target,step,step));
		//transform.RotateAround(transform.parent.position, transform.parent.up, 20 * Time.deltaTime);
		if (Input.GetKeyDown (KeyCode.Space)) {
			open = !open;
			openSwitch.state = open;
		}

		open = openSwitch.state;
	
	}
}
