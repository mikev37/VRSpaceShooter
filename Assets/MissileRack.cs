using UnityEngine;
using System.Collections;

public class MissileRack : Weapon {





	public GameObject[] ordnance;

	int missile = 0;

	public Rigidbody body;

	public float firerate;

	private float timer;

	public override float getMuzzleVelocity (){
		return 10000;
	}

	public override void fire (){
		if (timer == 0) {
			if (missile < ordnance.Length) {
				ordnance [missile].GetComponent<Activate> ().enabled = true;
				ordnance [missile].transform.parent = null;
				Rigidbody missileBody = ordnance [missile].GetComponent<Rigidbody> ();
				if (missileBody != null) {
					missileBody.isKinematic = false;
				}
				if (ordnance [missile].GetComponent<TargetLocked> () != null) {
					ordnance [missile].GetComponent<TargetLocked> ().target = target;
				}
				if (ordnance [missile].GetComponent<ActivateOnProximity> () != null) {
					ordnance [missile].GetComponent<ActivateOnProximity> ().target = target;
				}
				ordnance [missile] = null;
				missile++;
				timer = firerate;
			}
		}

	}

	public override int mass (){return 0;}

	public override void reload (){}

	public override void arm (bool armed){}

	public override bool isArmed (){return true;}

	public override void eject (){}

	public override void attach (){
	}

	public override bool salvo (){ 
		return false;
	}

	public override int ammo (){
		return ordnance.Length - missile;
	}

	public override int maxAmmo (){
		return ordnance.Length;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;

			if (timer <= 0) {
				timer = 0;
			}
		}
	}
}
