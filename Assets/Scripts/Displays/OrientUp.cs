using UnityEngine;
using System.Collections;
/*
 * An object with this will always point up
 */
public class OrientUp : MonoBehaviour {


	public Vector3 up;
	Quaternion upRotate;
	Transform pTransform;
	public bool forwardFace = false;
	// Use this for initialization
	void Start () {
		if (up == null) {
			up = Physics.gravity * -1;
		}

		upRotate = new Quaternion();
		upRotate.eulerAngles = up;//(up.x,up.y,up.z);

		pTransform = GetComponentInParent<SpaceShipCore> ().transform;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.localRotation = Quaternion.Inverse (pTransform.rotation) * upRotate;
		if (forwardFace) {
			Vector3 ySquishedVector = pTransform.forward;
			ySquishedVector.y = 0;
			transform.rotation = Quaternion.LookRotation (ySquishedVector, up);
		} else {
			transform.rotation = upRotate;
		}
	
	}
}
