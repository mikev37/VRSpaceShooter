using UnityEngine;
using System.Collections;
using System;

public class AircraftGun : Weapon {

	public GameObject MuzzleFlare;

	public AudioSource audio;

	public AudioSource audioGunBlast;

	public AudioSource audioFallOff;

	public Animator animator;

	public GameObject Projectile;

	public Transform barrel;

	public Rigidbody body;

	public float firerate;

	private float timer;

	public int maxAmmoConsume = 1;

	public int ammoNum, maxAmmoNum;

	public override float getMuzzleVelocity (){
		try{
			return Projectile.GetComponent<Launch>().charge / Projectile.GetComponent<Rigidbody>().mass;
		}catch(NullReferenceException e){
			return 100;
		}
	}

	public override void fire (){
		if (timer == 0) {
			ammoNum -= UnityEngine.Random.Range (1, maxAmmoConsume);
			audio.Play ();
			animator.speed = 1;
			MuzzleFlare.SetActive (true);
			GameObject bullet = (GameObject)Instantiate (Projectile, barrel.position, transform.rotation, null);
			if (body != null) {
				bullet.GetComponent<Rigidbody> ().velocity += body.GetPointVelocity (bullet.transform.position);
			}
			timer = firerate;
		}

	}

	public override bool salvo (){ 
		return true;
	}

	public override int ammo (){
		return ammoNum;
	}

	public override int maxAmmo (){
		return maxAmmoNum;
	}

	public override int mass (){return 0;}

	public override void reload (){}

	public override void arm (bool armed){}

	public override bool isArmed (){return true;}

	public override void eject (){}

	public override void attach (){}

	// Use this for initialization
	protected override void Start () {
		MuzzleFlare.SetActive (false);
		animator.speed = 0;
	}

	// Update is called once per frame
	protected override void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;

			if (timer <= 0) {
				timer = 0;
			}
		}

		if (animator.speed < .9f) {
			MuzzleFlare.SetActive (false);
		}

		animator.speed = Mathf.Lerp (animator.speed, 0, Time.deltaTime);


		if (Input.GetKey(KeyCode.Space)) {
			fire ();
		}
	}
}
