using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;
[RequireComponent(typeof (SwitchMapping))]
public class Battery : MonoBehaviour {

	public int capacity;
	public int dischargeRate;
	public int chargeRate;
	public int minEnergyValue;
	public float energyReserve;
	public LinearMapping linMap;
	public SwitchMapping engaged;
	public PowerSystem system;

	// Use this for initialization
	void Start () {
		if (system == null) {
			system = GetComponentInParent<SpaceShipCore> ().power;
		}
		engaged = GetComponent<SwitchMapping> ();
	}
	
	// Update is called once per frame
	public void powerUpdate () {
		if (linMap != null) {
			linMap.value = energyReserve / capacity;
		}

		if (engaged.on) {


			//Batteries always strive to bring down the net charge in the system to 0
			float power = system.getPower ();

			//if(system

			if (power > 0) {
				//charge

				//charge can be limited by the rate
				float trueChargeValue = Mathf.Min(power,chargeRate);
				//or by being out of the capacity
				trueChargeValue = Mathf.Min (trueChargeValue, capacity - energyReserve);

				system.usePower (trueChargeValue);

				energyReserve += trueChargeValue * Time.deltaTime;

			} else if(power < 0) {
				//discharge

				//charge can be limited by the rate
				float trueDischargeValue = Mathf.Min(power * -1,dischargeRate);
				//or by being out of the capacity
				trueDischargeValue = Mathf.Min (trueDischargeValue,energyReserve);

				system.addPower (trueDischargeValue);

				//system.addReserve(trueDischargeValue);

				energyReserve -= trueDischargeValue * Time.deltaTime;

			}
		}
	}
}
