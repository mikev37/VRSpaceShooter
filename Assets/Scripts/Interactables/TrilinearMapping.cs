using UnityEngine;
using System.Collections;

using Valve.VR.InteractionSystem;

public class TrilinearMapping : MonoBehaviour {

	public Vector3 value;

	public LinearMapping[] linMaps;

	public JoyStickMapping joyMap;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (joyMap != null) {
			value.x = joyMap.pitch / 90f;
			value.y = joyMap.yaw / 90f;
			value.z = joyMap.rotation / 90f;
			return;
		}

		if (linMaps != null && linMaps.Length > 2) {
			value.x = linMaps[0].value;
			value.y = linMaps[1].value;
			value.z = linMaps[2].value;
			return;
		}
	}
}
