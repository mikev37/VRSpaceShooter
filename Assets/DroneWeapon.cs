using UnityEngine;
using System.Collections;
[RequireComponent(typeof(EnemyFlier))]
public class DroneWeapon : MonoBehaviour {

	public bool engaged;

	public Weapon weapon;

	public float deviation = 10;

	private EnemyFlier flier;
	// Use this for initialization
	void Start () {
		flier = GetComponent<EnemyFlier> ();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject target = flier.target;

		if (target == null)
			return;

		if (!engaged)
			return;

		if (Vector3.Angle (transform.forward, obtainFiringSolution ()) < deviation && Vector3.Distance (transform.position, target.transform.position) < flier.engagementRange)
		{
			weapon.target = target.transform;
			weapon.fire();
		}
	}

	Vector3 obtainFiringSolution(){
		GameObject target = flier.target;

		Rigidbody rb = GetComponent<Rigidbody> ();

		Rigidbody tb = target.GetComponent<Rigidbody> ();

		if (tb != null) {
			//aim at the spot where they will be
			float projectileVelocity = 100;
			if(GetComponent<DroneWeapon>() != null){
				projectileVelocity = GetComponent<DroneWeapon>().weapon.getMuzzleVelocity();
			}
			float distanceToTarget = Vector3.Distance(target.transform.position,transform.position);
			float flightTime = distanceToTarget / projectileVelocity;
			Vector3 targetVelocity = tb.velocity;

			Vector3 percievedLocation = tb.position + tb.velocity * flightTime;

			return (percievedLocation - transform.position).normalized;
		} else {
			//aim at Transform location
			return (target.transform.position - transform.position).normalized;
		}
	}

}
