using UnityEngine;
using System.Collections;

using Valve.VR.InteractionSystem;
public class HeatManager : MonoBehaviour {

	HeatSystem system;
	Environment env;
	public GameObject emissiveObject;
	public float heatInSystem;
	public float heatAbsorbtionRate;
	public float heatAbsorbtionCapacity;
	public float heatBleedOffRate;
	public float heatPassiveBleedOffRate;
	public float heatActiveBleedOffRate;
	public bool isFuel;
	public bool isEnergy;

	public LinearMapping linMap;
	public SwitchMapping engaged;
	public SwitchMapping exposed;
	public PowerUse powerUser;
	public FuelUse fuelUser;

	// Use this for initialization
	void Start () {
		system = GetComponentInParent<SpaceShipCore> ().heat;
		env = GetComponentInParent<SpaceShipCore> ().environment;
	}

	public float getAbsorbtion(){
		//if the system is turned on
		if (engaged.on) {
			//Check for power
			if (powerUser != null) {
				if (!powerUser.powerActive ()) {
					return 0;
				}
			}

			//if can absorb more heat
			if (heatInSystem < heatAbsorbtionCapacity) {

				float heat = system.getHeat ();

				heat *= heatAbsorbtionRate * 1f / system.totalAbsorbtion;

				if (heat > 0) {

					float heatAbsorbed = Mathf.Min (heatAbsorbtionCapacity - heatInSystem, heatAbsorbtionRate);

					heatAbsorbed = Mathf.Min (heatAbsorbed, heat);

					return heatAbsorbed;
				}
			}
		} 
		return 0;
			
	}
	
	// Update is called once per frame
	void Update () {
		//set the heat gauges and such
		if (linMap != null) {
			linMap.value = heatInSystem / heatAbsorbtionCapacity;
		}

		//set the radiators to glow if desired
		if (emissiveObject != null) {
			//Renderer ren = emissiveObject.gameObject.GetComponent<Renderer> ();
			/*Color nC = emissiveObject.gameObject.GetComponent<Renderer> ().material.color;
			nC.r = heatInSystem / heatAbsorbtionCapacity;
			nC.g = heatInSystem / heatAbsorbtionCapacity / 3;
			nC.b = 0;
			emissiveObject.gameObject.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", nC);
			*/

			foreach(Renderer ren in emissiveObject.gameObject.GetComponentsInChildren<Renderer> ()){
				Color nC = ren.material.color;
				nC.r = heatInSystem / heatAbsorbtionCapacity;
				nC.g = heatInSystem / heatAbsorbtionCapacity / 3;
				nC.b = 0;
				ren.material.SetColor ("_EmissionColor", nC);
			}
		}

		//if the heat management system can be exposed to the outside, it might cool faster.
		if (exposed != null) {
			if (exposed.on) {
				if(heatInSystem > env.temperature){
					heatInSystem  = Mathf.Max(heatInSystem - (env.atmosphericDensity + heatActiveBleedOffRate) * Time.deltaTime,env.temperature);
				}
			}
		}

		//if the system can passively bleed off heat, it will do so
		if (heatInSystem > 0 && heatBleedOffRate > 0) {
			if (fuelUser != null) {
				float removed = Mathf.Min (heatBleedOffRate, heatInSystem);
				fuelUser.usage = removed;
				if (fuelUser.isFueled ()) {
					heatInSystem = Mathf.Max (heatInSystem - heatBleedOffRate * Time.deltaTime, 0);
				}
			} else {
				heatInSystem = Mathf.Max (heatInSystem - heatBleedOffRate * Time.deltaTime, 0);
			}
		}



		//if the system is turned on
		if (engaged.on) {

			if (!system.coolers.Contains (gameObject)) {
				system.totalAbsorbtion += heatAbsorbtionRate;
				system.coolers.Add (gameObject);
			}

			//Check for power
			if (powerUser != null) {
				if (!powerUser.powerActive ()) {
					isEnergy = powerUser.powerActive ();

					return;
				}
			}

			//if can absorb more heat
			if (heatInSystem < heatAbsorbtionCapacity) {

				float heat = system.getHeat ();

				heat *= heatAbsorbtionRate * 1f / system.totalAbsorbtion;
					
				if (heat > 0) {

					float heatAbsorbed = Mathf.Min (heatAbsorbtionCapacity - heatInSystem, heatAbsorbtionRate);

					heatAbsorbed = Mathf.Min (heatAbsorbed, heat);

					system.addHeat (-heatAbsorbed);

					heatInSystem += heatAbsorbed * Time.deltaTime;
				}
			}
		} else if (system.coolers.Contains (gameObject)) {
				system.totalAbsorbtion -= heatAbsorbtionRate;
				system.coolers.Remove (gameObject);

		}else{

			//if the system can passively bleed off heat, it will do so
			if (heatInSystem > 0 && heatPassiveBleedOffRate > 0) {
				heatInSystem = Mathf.Max (heatInSystem - heatPassiveBleedOffRate * Time.deltaTime, 0);
			}
		}
	}
}
