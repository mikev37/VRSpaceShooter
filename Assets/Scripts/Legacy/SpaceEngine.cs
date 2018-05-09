/*
 * DEPRECATED
 * 
 * using UnityEngine;
using BulletUnity;
using System.Collections;

public class SpaceEngine: MonoBehaviour {

	public Transform throttle;
	public Transform turnstick;

	public BRigidBody bship;
	public Rigidbody ship;

	public float gyropower;

	public int thrust;
	// Use this for initialization
	void Start () {
	}

	public static float turnsticktransform(float input){
		float i = (float) input;
		if (i > 300) {
			i = (60 - (i - 300)) / 10;

		} else if (i > 0) {
			i = (i * -1) / 10;
		} else
			i = 0;
		float abs = Mathf.Abs (i);

		if ( abs < .5f)
			return 0;
		return i/abs * (abs-.5f);
	}

	// Update is called once per frame
	void Update () {
		int power = (int)(throttle.localPosition.y * 10);

		float yaw = (int)turnstick.localRotation.eulerAngles.y; //Y
		float pitch = (int)turnstick.localRotation.eulerAngles.x; //X
		float roll = (int)turnstick.localRotation.eulerAngles.z; //Z

		pitch = SpaceEngine.turnsticktransform (pitch);
		yaw = SpaceEngine.turnsticktransform (yaw);
		roll = SpaceEngine.turnsticktransform (roll);
		/*
		ship.AddRelativeTorque(-1*Vector3.up * gyropower * yaw);
		ship.AddRelativeTorque(-1*Vector3.right * gyropower * pitch);
		ship.AddRelativeTorque(-1*Vector3.forward * gyropower * roll);

		ship.AddRelativeForce (Vector3.forward * thrust * power);

		Vector3 lR = Vector3.zero;

		lR -= (Vector3.up * gyropower * yaw);
		lR -= (Vector3.forward * gyropower * roll);
		lR -= (Vector3.right * gyropower * pitch);

		ship.transform.Rotate (lR);

		ship.transform.position += (ship.transform.forward * thrust * power);


		//bship.AddImpulse (bship.transform.forward * thrust * power);

		//Debug.Log (bship.transform.forward * thrust * power);

		//Debug.Log (Vector3.forward * thrust * power);

		Transform t = bship.transform;

		bship.AddForce (t.forward * thrust * power);

		bship.AddTorque(-1*bship.transform.up * gyropower * yaw);
		bship.AddTorque(-1*bship.transform.right * gyropower * pitch);
		bship.AddTorque(-1*bship.transform.forward * gyropower * roll);
	}
}


*/