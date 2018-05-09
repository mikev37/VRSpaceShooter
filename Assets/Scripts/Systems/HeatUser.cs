using UnityEngine;
using System.Collections;

[RequireComponent(typeof (SwitchMapping))]
public class HeatUser : MonoBehaviour {

	public HeatSystem system;
	public float usage;
	public SwitchMapping engaged; //Whether the system is meant to be turned on

	// Use this for initialization
	void Start () {
		if (system == null) {
			system = GetComponentInParent<SpaceShipCore> ().heat;
		}
		if (engaged == null) {
			engaged = GetComponent<SwitchMapping> ();
		}
	}
	public bool isNotOverheat(){
		return system.netHeat < system.danger;
	}

	// Update is called once per frame
	void Update () {
		if (engaged.on) {
			system.addHeat(usage);
		}
	}
}
