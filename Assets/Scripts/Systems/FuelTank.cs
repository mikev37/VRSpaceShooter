using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;
[RequireComponent (typeof( PowerUse))]
public class FuelTank : MonoBehaviour {

	public float fuelRemaining;
	public int maxFuel;
	public LinearMapping linMap;
	PowerUse power;
	public bool hasFuel(float usage){
		return usage < fuelRemaining && fuelRemaining > 0;
	}

	public void useFuel(float usage){
		fuelRemaining -= usage * Time.deltaTime;
		fuelRemaining = Mathf.Max (0, fuelRemaining);
	}

	// Use this for initialization
	void Start () {
		power = GetComponent<PowerUse> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (power.powerActive() && linMap != null) {
			linMap.value = fuelRemaining / maxFuel;
		}
	}
}
