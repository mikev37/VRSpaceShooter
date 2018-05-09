using UnityEngine;
using System.Collections;
/*
 * Gyroscopes help a ship turn regardless of atmosphere
 */

[RequireComponent (typeof (TrilinearMapping))]
[RequireComponent (typeof (PowerUse))]
public class Gyroscope : MonoBehaviour {

	public TrilinearMapping controlInput;
	public Rigidbody body;
	public SwitchMapping enabled;
	public SwitchMapping sas;
	public PID pid;
	public float pForce;
	public Vector2 powerUse;
	public PowerUse power;
	public float turnForce;
	public AudioSource audio;
	public float limiter;

	public float work;
	// Use this for initialization
	void Start () {
		controlInput = GetComponent<TrilinearMapping> ();
		body = GetComponentInParent<SpaceShipCore> ().GetComponent<Rigidbody> ();
		power = GetComponent<PowerUse> ();
		if (enabled == null) {
			enabled = GetComponent<SwitchMapping> ();
		}
	}

	void Update () {
		if (enabled.on) {
			power.usage = powerUse.x + (powerUse.y - powerUse.x) * work;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		work = 0;
		if (enabled.on && power.powerActive()) {
			float turnForceFinal = turnForce * limiter;
			body.AddRelativeTorque (controlInput.value.x * turnForceFinal, controlInput.value.y * turnForceFinal, controlInput.value.z * turnForceFinal);
			work = controlInput.value.magnitude / 3;

			if (sas != null && sas.on) {
				float pidForce = pid.Update(0,body.angularVelocity.magnitude,Time.fixedDeltaTime);
				pForce = pidForce;
				pidForce = Mathf.Min (Mathf.Abs(pidForce), 3) * (1 - Mathf.Min(.8f,Mathf.Pow(work,2)));
				body.AddTorque (-body.angularVelocity.normalized * turnForce * pidForce);
				work += pidForce / 3;
			}
		}

		if (audio != null) {
			audio.volume = work;
			audio.pitch = .75f + .5f * work;
		}
	}
}
