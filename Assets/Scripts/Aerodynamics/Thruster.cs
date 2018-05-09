using UnityEngine;
using System.Collections;

using Valve.VR.InteractionSystem;

/*
 * This thruster will use heat, power and fuel to provide a force at location to the object.
 * 
 * 
 */
[RequireComponent(typeof(HeatUser))]
[RequireComponent(typeof(PowerUse))]
[RequireComponent(typeof(FuelUse))]
public class Thruster : MonoBehaviour {

	public float vacIsp = .1f;

	public Vector2 fuelUse;
	public Vector2 energyUse;
	public Vector2 heatUse;

	public Vector3 thrustPower;

	public SwitchMapping engaged;

	public LinearMapping throttle;

	public ParticleSystem[] particles;
	public Vector2 particlesEmit;
	public Vector2 particleSpeed;

	public AudioClip start;

	public AudioClip idle;

	public AudioSource audioSource;
	public Vector2 audioVolume;
	public Vector2 audioPitch;

	Rigidbody ship;

	HeatUser heat;
	PowerUse power;
	FuelUse fuel;

	// Use this for initialization
	void Start () {
		if (engaged == null) {
			engaged = new SwitchMapping ();
		}
		if (throttle == null) {
			throttle = new LinearMapping ();
		}

		heat = GetComponent<HeatUser> ();
		power = GetComponent<PowerUse> ();
		fuel = GetComponent<FuelUse> ();
		ship = GetComponentInParent<SpaceShipCore> ().GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		bool canFly = power.powerActive () && fuel.isFueled ();

		if (engaged.on && canFly) {
			
			heat.usage = heatUse.x + (heatUse.y - heatUse.x) * throttle.value;
			power.usage = energyUse.x + (energyUse.y - energyUse.x) * throttle.value;
			fuel.usage = fuelUse.x + (fuelUse.y - fuelUse.x) * throttle.value;

			foreach (ParticleSystem pS in particles) {
				ParticleSystem.EmissionModule emissionModule = pS.emission;
				emissionModule.rate = particlesEmit.x + particlesEmit.y * throttle.value;
				pS.startSpeed = particleSpeed.x + particleSpeed.y * throttle.value;
				//pS.emission.rate.constantMax = particlesEmit.y * throttle.value;
			}
			if (!audioSource.isPlaying) {
				audioSource.Play ();
			}
			audioSource.volume = audioVolume.x + (audioVolume.y - audioVolume.x) * throttle.value;
			audioSource.pitch = audioVolume.x + (audioVolume.y - audioVolume.x) * throttle.value;

		} else {
			heat.usage = 0;
			power.usage = 0;
			fuel.usage = 0;

			foreach (ParticleSystem pS in particles) {
				ParticleSystem.EmissionModule emissionModule = pS.emission;
				emissionModule.rate = 0;
				pS.startSpeed = 0;
			}

			//audioSource.Stop ();
			audioSource.volume = 0;
		}



	}

	// Update is called once per frame
	void FixedUpdate () {
		bool canFly = power.powerActive () && fuel.isFueled ();

		if (engaged.on && canFly) {

			float airIsp = 1;

			float impulse = airIsp * (Environment.Atmosphere) + vacIsp * (1 - Environment.Atmosphere);

			Vector3 pushForce = transform.rotation * thrustPower * impulse;

				
				
			ship.AddForceAtPosition (pushForce * throttle.value, this.transform.position);
		} else {
				
		}
	}
}
