using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampIndicator : MonoBehaviour {

	public SwitchMapping swiMap;
	public GameObject emissiveObject;
	public Color color;

	void Start (){
		if (color == null)
			color = Color.white;
	}
	// Update is called once per frame
	void Update () {
		if (swiMap != null) {
			if (emissiveObject != null) {
				Material mat = emissiveObject.gameObject.GetComponent<Renderer> ().material;
				mat.color = color;
				mat.SetColor ("_EmissionColor", color);
				emissiveObject.gameObject.SetActive (swiMap.on);
			}
		}
	}
}
