using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/*
 * Prints current weapon information from the weaponsystem
 */
public class WeaponReadout : MonoBehaviour {

	public WeaponsSystem ws;

	public Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "" +
		ws.getName () + "\n" +
		ws.getAmmo ();
	}
}
