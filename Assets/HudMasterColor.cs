using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudMasterColor : MonoBehaviour {
	public Color color;
	// Use this for initialization
	void Start () {
		ColorHudElement[] ches = GetComponentsInChildren<ColorHudElement> ();
		foreach (ColorHudElement  che in ches) {
			che.Paint (color);
		}
	}
}
