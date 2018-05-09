using UnityEngine;
using System.Collections;

[RequireComponent (typeof (TrilinearMapping))]
public class TrilinearInputAdapter : MonoBehaviour {
	TrilinearMapping map;
	// Use this for initialization
	void Start () {
		map = GetComponent<TrilinearMapping> ();
	}
	
	// Update is called once per frame
	void Update () {
		map.value.x = Input.GetAxis ("Vertical");
		map.value.z = Input.GetAxis ("Horizontal");
	}
}
