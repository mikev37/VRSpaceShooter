using UnityEngine;
using System.Collections;

[RequireComponent(typeof (PowerUse))]
public class ExternalPower : MonoBehaviour {

	public Transform powerSource;

	PowerUse power;

	Transform spaceship;

	public int provided;

	public int distance;

	public SwitchMapping indicator;

	// Use this for initialization
	void Start () {
		power = GetComponent<PowerUse> ();
		spaceship = GetComponentInParent<SpaceShipCore> ().transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (powerSource == null)
			return;


		if (Vector3.Distance (spaceship.transform.position, powerSource.transform.position) < distance) {
			power.usage = -1 * provided;
			if (indicator != null) {
				indicator.on = true;
			}
		} else {
			power.usage = 0;
			if (indicator != null) {
				indicator.on = false;
			}
		}
	}
}
