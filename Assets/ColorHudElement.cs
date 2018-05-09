using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorHudElement : MonoBehaviour{

	void Awake(){
		this.Paint (GetComponentInParent<HudMasterColor> ().color);
	}

	// Use this for initialization
	public void Paint (Color color) {
		GetComponentInChildren<Image> ().color = color;
	}
}
