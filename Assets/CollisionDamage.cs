using UnityEngine;
using System.Collections;
using JackPotGames.VRSpaceFlyer;

[RequireComponent(typeof (DamageSensor))]
public class CollisionDamage : MonoBehaviour {

	public DamageSensor sensor;

	void Start(){
		if(sensor == null)
			sensor = GetComponent<DamageSensor> ();
	}
	
	// Update is called once per frame
	void onCollisionEnter (Collision collision) {
		sensor.Damage (collision.impulse.magnitude / 10f);
	}
}
