using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunsight : MonoBehaviour {
	public Sprite displaySprite;
	public bool gravityAffect;
	WeaponsSystem system;
	Weapon current;
	public int drawDistance;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponentInParent<SpaceShipCore> ().body;
		system = GetComponentInParent<SpaceShipCore> ().GetComponentInChildren<WeaponsSystem> ();
		current = system.arsenal [system.currentSelection] [0];
	}

	void calculateImpact(Rigidbody me,int impactHeight,out Vector3 result){
		float y = me.position.y - impactHeight;
		float dY = me.velocity.y;
		float x = me.position.x;
		float dX = me.velocity.x;
		float z = me.velocity.z;
		float dZ = me.velocity.z;
		float g = Physics.gravity.magnitude;

		float plus = (-dY + Mathf.Sqrt(Mathf.Pow(dY,2) + 2 * g * y)) / (2 * y);
		float minus = (-dY - Mathf.Sqrt(Mathf.Pow(dY,2) + 2 * g * y))/ (2 * y);

		float t = Mathf.Max (plus, minus);

		result.x = x + t * dX;
		result.y = impactHeight;
		result.z = z + t * dZ;

	}

	bool determineIgnoreGravity(){
		//if (current == null)
		//	return false;
		//if(current.GetComponent<
		//TODO
		//add way to get projectile speed at launch to the system
		//add way to determine whether the projectile is powered or not


		return false;
	}
	
	// Update is called once per frame
	void Update () {
		current = system.arsenal [system.currentSelection] [0];
		//set image
		gravityAffect = determineIgnoreGravity();
		if (gravityAffect && Physics.gravity.sqrMagnitude > 0) {
			//calculate position to ground
			//gameObject.transform.position = calculateImpact (rb, 0);
		} else {
			//just draw it forward of the craft
			gameObject.transform.localPosition = Vector3.forward * drawDistance;
		}
	}
}
