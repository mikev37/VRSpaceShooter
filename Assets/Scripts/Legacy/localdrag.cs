using UnityEngine;
using System.Collections;

public class localdrag : MonoBehaviour {
	public Rigidbody body;
	public Rigidbody ship;
	float dragfactor = 0.01f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		body.velocity = body.velocity * dragfactor;
	
	}
}
