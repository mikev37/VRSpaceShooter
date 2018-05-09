using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinualTextureOffset : MonoBehaviour {

	public Vector2 Offset;

	Renderer render;

	// Use this for initialization
	void Start () {
		render = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		render.material.mainTextureOffset = Offset * Time.time;//.SetTextureOffset (0, Offset * Time.deltaTime);
	}
}
