using UnityEngine;
using System.Collections;

[RequireComponent(typeof (SwitchMapping))]
public class FuelUse : MonoBehaviour {

	public FuelTank system;
	public float usage;
	public SwitchMapping engaged; //Whether the system is meant to be turned on

	// Use this for initialization
	void Start () {
		if (system == null) {
			system = GetComponentInParent<SpaceShipCore> ().fueltank;
		}
		if (engaged == null) {
			engaged = GetComponent<SwitchMapping> ();
		}
	}

	public bool isFueled(){
		return engaged.on && system.hasFuel (usage);
	}

	// Update is called once per frame
	void Update () {
		if (engaged.on) {
			system.useFuel(usage);
		}
	}
}
