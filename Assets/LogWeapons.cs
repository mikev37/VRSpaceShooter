using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogWeapons : MonoBehaviour {
	Weapon[] weapons;
	public Button[] buttons;

	public Weapon selected;



	// Use this for initialization
	void Start () {
		foreach (Button m in buttons) {
			m.gameObject.SetActive (false);
		}
		weapons = GetComponentInParent<SpaceShipCore> ().GetComponentsInChildren<Weapon> ();
		for (int i = 0; i < weapons.Length; i++){
			buttons [i].GetComponentInChildren<Text> ().text = weapons [i].name;
			buttons [i].gameObject.SetActive(true);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
