using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/*
 * This will read out the Altitide and speed of a craft
 */
public class readout : MonoBehaviour {
	Text text;

	public Rigidbody engine;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {

		int power = (int)engine.transform.position.y;
	
		int speed = (int) engine.velocity.magnitude;

		text.text = "ALT:" + power + "\n"
		+ "SPD:" + speed + "\n";
	}
}
