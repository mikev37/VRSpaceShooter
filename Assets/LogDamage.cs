using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JackPotGames.VRSpaceFlyer;

public class LogDamage : MonoBehaviour {
	Text textfield;
	DamageSensor[] sensors;
	SpaceShipCore ship;
	// Use this for initialization
	void Start () {
		textfield = GetComponent<Text> ();
		ship = GetComponentInParent<SpaceShipCore> ();
		sensors = ship.GetComponentsInChildren<DamageSensor> ();
	}
	
	// Update is called once per frame
	void Update () {
		textfield.text = "HULL INTEGRITY: " + ship.getHullIntegrity () + "\n";

		foreach (DamageSensor sensor in sensors) {
			textfield.text += sensor.name.PadRight (20, ' ') + (sensor.health + "/" + sensor.maxHealth).PadLeft (10, ' ') + "\n";
		}
	}
}
