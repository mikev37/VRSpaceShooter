using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Valve.VR.InteractionSystem;

public class HeatSystem : MonoBehaviour {

	public float netHeat;
	public float warning;
	public float danger;

	public LinearMapping linMap;

	public List<GameObject> coolers;
	public float totalAbsorbtion;

	Environment env;

	public void addHeat(float usage){
		netHeat += usage * Time.deltaTime;
	}

	public float getHeat(){
		return netHeat;
	}

	// Use this for initialization
	void Start () {
		env = GetComponentInParent<SpaceShipCore> ().environment;
		coolers = new List<GameObject> ();
	}



	// Update is called once per frame
	void Update () {
		if (linMap != null) {
			linMap.value = Mathf.Min(netHeat / warning,1f);
		}

		//Bleed off heat into the atmosphere
		if (Environment.Atmosphere > 0) {

			float diff = Mathf.Abs (Environment.Temperature - netHeat) * .2f;

			if(netHeat > Environment.Temperature){
				netHeat -= Environment.Atmosphere * Time.deltaTime * diff;
			}
			else if(netHeat < Environment.Temperature){
				netHeat += Environment.Atmosphere  * Time.deltaTime * diff;
			}
		}
	}
}
