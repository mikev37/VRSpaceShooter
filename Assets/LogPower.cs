using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LogPower : MonoBehaviour {
	public Text textfield;
	public Text userfield;

	public PowerUse[] users;
	public PowerSystem system;

	public Battery[] batteries;
	// Use this for initialization
	void Start () {
		system = GetComponentInParent<SpaceShipCore> ().power;

	}
	
	// Update is called once per frame
	void Update () {
		batteries = system.batteries;
		users = system.network;
		textfield.text = "POWER: \n\n";
		textfield.text += "Producers".PadRight(30,'-')+" \n";
		userfield.text = "Users".PadRight(30,'-')+"\n";
		foreach (PowerUse p in users) {
			if (p.usage < 0) {
				textfield.text += p.name.PadRight(20,' ') + string.Format("{0:0.#}",(-1 * p.usage)).PadLeft(5,' ') + "\n";
			} else {
				userfield.text += p.name.PadRight(20,' ') + string.Format("{0:0.#}",( p.usage)).PadLeft(5,' ') + "\n";
			}
		}
		textfield.text += "Batteries".PadRight(30,'-')+"\n";
		foreach (Battery b in batteries) {
			textfield.text += ( "+"+b.dischargeRate +"/-" + b.chargeRate + "Mw/H").PadLeft(10,' ' ) + (b.energyReserve + "/" + b.capacity + "MW").PadLeft(10,' ' ) + "\n";
		}
	}
}
