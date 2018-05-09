using UnityEngine;
using System.Collections;

/*
 * This makes a UI item keep a similar percieved size profile as it moves further away.
 */
#pragma warning disable
public class UnscaleWithDistance : MonoBehaviour {
	public float mult = 1;//.25f;
	public float add = 0;//1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.main != null) {
			GameObject focalPoint = Camera.main.gameObject;
			float relSize = mult * Vector3.Distance (focalPoint.transform.position, transform.position) + add;
			transform.localScale = Vector3.one * relSize;
		}
	}
}
