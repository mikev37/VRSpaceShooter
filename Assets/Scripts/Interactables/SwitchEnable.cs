using UnityEngine;
using System.Collections;

[RequireComponent( typeof (SwitchMapping) ) ]
public class SwitchEnable : MonoBehaviour {

	SwitchMapping swiMap;
	bool val;
	public GameObject[] list;

	void Start () {
		swiMap = GetComponent<SwitchMapping> ();
		if (swiMap != null) {
			val = !swiMap.on;
		}
	}

	// Update is called once per frame
	void Update () {
		if (swiMap != null) {
			if(val != swiMap.on)
			{
				val = swiMap.on;
				foreach (GameObject go in list) {
						go.SetActive (val);
				}
			}
		}
	}
}
