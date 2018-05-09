using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackPotGames.VRSpaceFlyer;
public class LogEngineering : MonoBehaviour {

	Text textfield;

	PowerSystem power;

	HeatSystem heat;

	FuelTank fuels;

	SpaceShipCore ship;

	// Use this for initialization
	void Start () {
		textfield = GetComponent<Text> ();
		power = GetComponentInParent<SpaceShipCore> ().power;
		heat = GetComponentInParent<SpaceShipCore> ().heat;
		fuels = GetComponentInParent<SpaceShipCore> ().fueltank;
		ship = GetComponentInParent<SpaceShipCore> ();
	}
	
	// Update is called once per frame
	void Update () {
		textfield.text = "POWER: \n" +
		"In: " + power.netContribution + "\n" +
		"Out: " + power.netDrain + "\n";

		if (power.overload) {
			textfield.text += "!!!POWER OVERLOAD!!!\n\n";
		} else {
			textfield.text += "\n\n";
		}

		textfield.text += "TEMP: " + heat.netHeat + " C\n";

		if (heat.netHeat > heat.warning) {
			textfield.text += "!!!!HEAT DANGER!!!!\n\n";
		}else {
			textfield.text += "\n\n";
		}

		textfield.text += "FUEL: " + fuels.fuelRemaining + "/" + fuels.maxFuel + "L\n";

		if (fuels.fuelRemaining < .1f * fuels.maxFuel) {
			textfield.text += "!!!FUEL WARNING!!!\n";
		}else {
			textfield.text += "\n";
		}

		textfield.text += "HULL: " + ship.getHullIntegrity() + "%\n";

		if (ship.getHullIntegrity() < 10) {
			textfield.text += "!!!HULL CRITICAL!!!\n";
		}else {
			textfield.text += "\n";
		}


	}
}
