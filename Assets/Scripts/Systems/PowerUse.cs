using UnityEngine;
using System.Collections;

[RequireComponent(typeof (SwitchMapping))]
public class PowerUse : MonoBehaviour {

	public PowerSystem system;
	public float usage;
	public SwitchMapping engaged; //Whether the system is meant to be turned on

	HeatUser tempCheck;

	// Use this for initialization
	void Start () {
		if (system == null) {
			system = GetComponentInParent<SpaceShipCore> ().power;
		}
		if (engaged == null) {
			engaged = GetComponent<SwitchMapping> ();
		}
		if (tempCheck == null) {
			tempCheck = GetComponent<HeatUser> ();
		}
	}

	public bool powerActive(){
		if (tempCheck != null) {
			return tempCheck.isNotOverheat() && engaged.on && !system.overload;
		}

		return engaged.on && !system.overload ;
	}

	public bool isPowered(){
		return !system.overload ;
	}
	
	// Update is called once per frame
	public void powerUpdate () {
		if (engaged.on) {
			system.usePower (usage);

			float power = system.getPower ();
		}
	}
}
