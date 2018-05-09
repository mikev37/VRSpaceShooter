using UnityEngine;
using System.Collections;

using Valve.VR.InteractionSystem;

public class ThrusterGauge : LinearMapping {

	public LinearMapping thruster;

	public LinearMapping spoolUp;

	public GaugeIndicator gauge;

	
	// Update is called once per frame
	void Update () {
		if (spoolUp.value < 1) {
			value = spoolUp.value;
		} else {
			value = Mathf.Lerp (value, thruster.value * .9f + .1f, Time.deltaTime);
		}
	}
}
