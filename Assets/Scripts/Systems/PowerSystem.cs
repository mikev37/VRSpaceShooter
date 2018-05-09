using UnityEngine;
using System.Collections;

public class PowerSystem : MonoBehaviour {

	public float netPower;
	public float exPower;
	public bool overload;

	public float netDrain;

	public float netContribution;

	public int safety = 10;

	public PowerUse[] network;

	public Battery[] batteries;

	public bool hasPower(float usage){
		return netPower>0 && netPower - usage > 0;
	}

	public void usePower(float usage){
		netDrain += usage;
		netPower -= usage;
	}

	public void addPower(float usage){
		netContribution += usage;
		netPower += usage;
	}

	public float getPower(){
		return netPower;
	}

	// Use this for initialization
	void Start () {
		//netPower = 100;
		network = GetComponentInParent<SpaceShipCore>().gameObject.GetComponentsInChildren<PowerUse> ();
		batteries = GetComponentInParent<SpaceShipCore>().GetComponentsInChildren<Battery> ();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		exPower = netPower;
		netPower = netContribution - netDrain;
		netDrain = 0;
		netContribution = 0;

		if (exPower < 0) {
			if (!overload) {
				safety--;
				if (safety <= 0) {
					overload = true;
				}
			} 
		} else {
			safety = 10;
			overload = false;
		}
		*/
		overload = false;
		netPower = 0;
		netDrain = 0;
		netContribution = 0;

		foreach (PowerUse pu in network) {
			pu.powerUpdate();
		}

		foreach (Battery bat in batteries) {
			bat.powerUpdate();
		}

		if (netPower < 0) {
			overload = true;
		}




	}
}
