using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LogFuel : MonoBehaviour {
	Text textfield;
	FuelTank[] fuels;

	// Use this for initialization
	void Start () {
		textfield = GetComponent<Text> ();
		fuels = GetComponentInParent<SpaceShipCore> ().GetComponentsInChildren<FuelTank> ();
	}
	
	// Update is called once per frame
	void Update () {
		textfield.text = "";
		foreach (FuelTank f in fuels) {
			textfield.text += f.name + "\n";
			textfield.text += f.fuelRemaining + "/" + f.maxFuel + "\n";
		}
	}
}
