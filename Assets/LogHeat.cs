using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LogHeat : MonoBehaviour {

	Text textfield;

	HeatSystem heat;

	HeatUser[] cookers;

	HeatManager[] coolers;

	SpaceShipCore ship;

	// Use this for initialization
	void Start () {
		textfield = GetComponent<Text> ();
		heat = GetComponentInParent<SpaceShipCore> ().heat;
		cookers = GetComponentInParent<SpaceShipCore> ().GetComponentsInChildren<HeatUser> ();
		coolers = GetComponentInParent<SpaceShipCore> ().GetComponentsInChildren < HeatManager> ();
	}

	// Update is called once per frame
	void Update () {
		string tempReadout = string.Format ("{0:0.#}", heat.netHeat) + "       ";
		tempReadout.Substring (0, 7);
		textfield.text = "TEMPERATURE: " + tempReadout + " Celsius\n";
		textfield.text += "Outside Temperature: " + Environment.Temperature + " \n";

		/*
		 * TODO Draw a ascii thermometer like
		 * |==========     |       |
		 * 0             200     300
		textfield.text += "|";
		for(

		textfield.
		*/
		textfield.text += "Systems Giving Off Heat\n";
		foreach (HeatUser cook in cookers) {
			textfield.text += cook.name + " : +" + cook.usage + "C \n";
		}
		textfield.text += "Systems Absorbing Heat\n";
		foreach (HeatManager cool in coolers) {
			textfield.text += cool.name + " : -" + cool.getAbsorbtion () + "| " +  cool.heatInSystem + "C\n";
		}

	}
}
