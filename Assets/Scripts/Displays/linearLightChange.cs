using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;
/*
 * This can be used on an indicator lamp to vary intensity of light 0 to 1 or have a boolean on/off state
 */
[RequireComponent (typeof(Light))]
public class linearLightChange : MonoBehaviour {

	public LinearMapping linMap;
	public SwitchMapping swiMap;
	public CircularDrive cirMap;
	public float minThreshold;
	public float maxThreshold;
	public GameObject emissiveObject;
	public Color color;
	Light light;
	void Start (){
		light = GetComponent<Light> ();
		if(color != null)
			light.color = color;
	}
	// Update is called once per frame
	void Update () {
		if (linMap != null) {
			
			if (minThreshold > 0) {
				light.enabled = linMap.value < minThreshold;
			} else {
				light.intensity = linMap.value*100;
			}
				
		}

		if (cirMap != null) {
			light.intensity = cirMap.outAngle;
		}

		if (swiMap != null) {
			light.enabled = swiMap.on;
		}


		if (emissiveObject != null) {
			emissiveObject.gameObject.GetComponent<Renderer> ().material.color = color;
			emissiveObject.gameObject.SetActive (light.enabled);
		}
	}
}
