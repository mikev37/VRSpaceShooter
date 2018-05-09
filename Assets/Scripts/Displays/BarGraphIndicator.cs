using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;
/*
 * This will scale an inner portion of a bar graph to represent a value 0 to 1
 */
public class BarGraphIndicator : MonoBehaviour {

	public LinearMapping linMap;
	public float scale;
	public Transform centerHinge;
	Vector3 adjustedScale = new Vector3(1,1,1);
	// Update is called once per frame

	void Update () {

		if (linMap != null) {
			scale = linMap.value ;
		}
		adjustedScale.z = scale;
		centerHinge.localScale = adjustedScale;

	}
}
