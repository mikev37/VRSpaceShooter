using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;
/*
 * Make an arrow spin to indicate a value 0 t0 1
 */
public class GaugeIndicator : MonoBehaviour {

	public LinearMapping linMap;
	public float angle;
	public Transform centerHinge;
	// Update is called once per frame
	void Update () {

		if (linMap != null) {
			angle = linMap.value * 270f - 180;
		}
		centerHinge.localEulerAngles = Vector3.forward * angle;
	}
}
