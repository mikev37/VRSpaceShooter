using UnityEngine;
using System.Collections;

public class PlayerCatcher : MonoBehaviour {

	GameObject spaceship;

	// Use this for initialization
	void Start () {
		spaceship = GetComponentInParent<SpaceShipCore> ().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player")
			other.transform.SetParent (spaceship.transform);
	}

	void OnTriggerExit(Collider other) {
		//Maybe allow player to leave cockpit
	}
}
