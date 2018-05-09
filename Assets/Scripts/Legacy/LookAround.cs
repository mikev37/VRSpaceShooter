using UnityEngine;
using System.Collections;

public class LookAround : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public int speed = 10;
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (Input.GetAxis ("Mouse Y"), Input.GetAxis ("Mouse X"), 0) * Time.deltaTime * speed);
	}
}
