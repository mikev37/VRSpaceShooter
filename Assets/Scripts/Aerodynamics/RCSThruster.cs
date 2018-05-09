using UnityEngine;
using System.Collections;

/*
 * These thrusters will provide a sense of drag in space to make flying more managable.
 */
[RequireComponent (typeof(FuelUse))]
[RequireComponent (typeof(PowerUse))]
[RequireComponent (typeof(AudioSource))]
public class RCSThruster : MonoBehaviour {

	public Environment env;

	Vector3 desiredThrust;

	public TrilinearMapping rotationMap;

	public TrilinearMapping movementMap;

	Quaternion desiredThrustRotation;

	public Transform thruster;

	public PID dragPID, rotatePID;

	public Transform centerAngle;

	Rigidbody body;

	public Vector2 powerDrain;

	public Vector2 fuelDrain;

	public Vector2 particleCount;

	public Vector2 particleSpeed;
	/* children has to have exactly one particle system */
	ParticleSystem particles;

	AudioSource audio;

	public float vectorSpeed;

	public float thrustForce;

	public float limiter;

	public SwitchMapping enabled;

	PowerUse powerUse;

	FuelUse fuelUse;

	float work;

	public float thrusterNum;

	public float adjustment;

	public float coaxialThreshold = 25;

	float neededForce;

	public bool induceDrag,decreaseRot,antiGrav,thrustAssit,forShow;//rotateAssist not done

	// Use this for initialization
	void Start () {
		env = GetComponentInParent<SpaceShipCore> ().environment;
		body = GetComponentInParent<SpaceShipCore> ().GetComponent<Rigidbody> ();
		particles = GetComponentInChildren<ParticleSystem> ();
		audio = GetComponent<AudioSource> ();
		desiredThrustRotation = new Quaternion();
		dragPID = new PID (1, 1, 0);
		rotatePID = new PID (1, 1, 0);
		if (thruster == null)
			thruster = transform;
		powerUse = GetComponent<PowerUse> ();
		fuelUse = GetComponent<FuelUse> ();
	}

	public void setDesiredOrientation(Vector3 wish){
		desiredThrust = wish;
		desiredThrustRotation.SetLookRotation (wish);
	}

	// Update is called once per frame
	void Update () {

		//Check that the component is turned on at all

		float throttle = 0;
		neededForce = 0;
		work = 0;

		if (enabled.on && powerUse.powerActive ()) {


			Vector3 accumulateForce = Vector3.zero;

			//Induced Drag

			if (induceDrag) {
				Vector3 pointVelocity = body.GetPointVelocity (thruster.position);
				float desiredForce = Mathf.Abs(dragPID.Update (0, pointVelocity.sqrMagnitude, Time.deltaTime));
				work += desiredForce * 10;
				accumulateForce += pointVelocity * desiredForce;
			}


			//Rotational Correct
			if (decreaseRot) {
				Vector3 rotation = body.GetPointVelocity (thruster.position) - body.velocity;
				float desiredForce = Mathf.Abs(dragPID.Update (0, rotation.magnitude, Time.deltaTime));
				work += desiredForce;
				accumulateForce += rotation * desiredForce;
			}
	
			//Fight Gravity

			if (antiGrav) {
				Vector3 physics = env.gravity;
				float physCoefficient = env.gravity.magnitude * body.mass / thrusterNum;
				work += physCoefficient;
				accumulateForce += physics * physCoefficient;
			}



			//TODO Rotational Assist <- THIS SECTION DOES NOT WORK
			/*
			desiredTorqueDirection = rotationMap.value.normalized;
			Vector3 temp = (transform.localPosition);
			positionVector = temp;
			finalRotationAssistDirection = body.transform.InverseTransformDirection(Vector3.Cross (positionVector, desiredTorqueDirection).normalized);
			this.setDesiredOrientation (finalRotationAssistDirection);
			*/
			//Thrust Assist

			if (thrustAssit && movementMap.value.sqrMagnitude > .01) {
				Vector3 forwardThrust = body.transform.TransformDirection (movementMap.value);

				float thrustCoefficient = movementMap.value.magnitude * thrustForce * limiter;
				accumulateForce += -1 * forwardThrust * thrustCoefficient;
				work += thrustCoefficient;
			}

			this.setDesiredOrientation (accumulateForce);

			//Calculate Gimbal lock

			Quaternion difference = desiredThrustRotation * Quaternion.Inverse (centerAngle.rotation);

			Vector3 angleDiff = difference.eulerAngles;

			angleDiff.z = 0;

			float angleDiffF = (angleDiff.magnitude + 180) % 360 - 180;

			//Thruster orientation
			if (desiredThrust.sqrMagnitude > 0) { //
				thruster.rotation = Quaternion.Slerp (thruster.rotation, desiredThrustRotation, vectorSpeed * Time.deltaTime);
			} else {
				thruster.localRotation = Quaternion.Slerp (thruster.localRotation, desiredThrustRotation, vectorSpeed * Time.deltaTime);
			}

			if(thrustForce > 0){

				if (fuelUse.isFueled ()) {
					neededForce = Mathf.Min (thrustForce, Mathf.Abs(work));
					throttle = neededForce / thrustForce;
				}
				//Drain Fuel and energy appropriately
				powerUse.usage = powerDrain.x + (powerDrain.y - powerDrain.x) * throttle;
				fuelUse.usage = fuelDrain.x + (fuelDrain.y - fuelDrain.x) * throttle;
			}

		}

		if (Quaternion.Angle (desiredThrustRotation, thruster.rotation) < coaxialThreshold  && neededForce > 0) {
			
			//Thruster firing add sound and modify particles and such
			ParticleSystem.EmissionModule emissionModule = particles.emission;
			emissionModule.rate = particleCount.x + particleCount.y * throttle;
			particles.startSpeed = particleSpeed.x + particleSpeed.y * throttle;

			audio.volume = throttle;
		} else {
			//Thruster firing add sound and modify particles and such
			ParticleSystem.EmissionModule emissionModule = particles.emission;
			emissionModule.rate = 0;
			particles.startSpeed = 0;

			audio.volume = 0;
		}

	}

	// Update is called once per frame
	void FixedUpdate () {
		//TODO limit angle to 
		if (neededForce > 0 && Quaternion.Angle (desiredThrustRotation, thruster.rotation) < coaxialThreshold) {
			if (!forShow) {
				body.AddForceAtPosition (-1 * thruster.forward * neededForce, thruster.position);
			}
		} else {
		}
	}
}
