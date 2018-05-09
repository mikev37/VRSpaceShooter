using UnityEngine;
using System.Collections;

public class WeaponRack : MonoBehaviour {

	public Weapon weapon;

	public Transform weaponLocation;

	public void fire(){
		if (weapon != null) {
			weapon.fire ();
		}
	}

	public void eject(){
		if (weapon == null)
			return;
		weapon.transform.parent = null;
		weapon.eject();
		weapon = null;
	}

	public void attach(){
		if (weapon != null)
			return;
		weapon.transform.parent = this.transform;
		weapon.transform.position = weaponLocation.position;
		weapon.transform.rotation = weaponLocation.rotation;
		weapon.attach ();
		weapon = null;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
