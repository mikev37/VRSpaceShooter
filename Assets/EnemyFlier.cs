using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/**
 * This is the main AI module for the enemy fliers
 */
[RequireComponent(typeof(Rigidbody))]
public class EnemyFlier : MonoBehaviour {
	[Header("BOID Behaviour")]

	[Range(0,10)]public float boid = 6.5f;
	public int minDistance = 100;


	[Header("Random Behavoir")]
	[Range(0,10)]public float random = 1;
	public int randInterval = 5;
	private float randTimer;
	private Vector3 rand3;


	[Header("STEERING Behavior")]
	[Range(0,50)]public float groundAvoidance;
	[Range(0,50)]public float collisionAvoidance = 25;
	[Range(0,10)]public float chase = 8;
	public int engagementRange = 500;
	public int minEngagementRange = 50;
	public GameObject target;
	[Range(0,10)]public float center;
	public int maxDistance;
	public GameObject battleFieldCenter;
	[Header("AERODYNAMICS")]
	[Range(0,90)]public float angleOfAttack = 5;
	[Range(0,1)]public float banking;
	public float turnSpeed = .1f;
	public float minHeight;
	[Header("IFF")]
	public string friend;
	public string  foe;

	static bool DEBUG;



	public HashSet<GameObject> friendHashSet,foeHashSet;

	public string friendHashSetDebug,foeHashSetDebug;

	private Vector3 desiredVelocity;

	// Use this for initialization
	void Start () {
		friendHashSet = new HashSet<GameObject>();
		foeHashSet = new HashSet<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if (friendHashSet == null) {
			friendHashSet = new HashSet<GameObject>();
		}
		if (foeHashSet == null) {
			foeHashSet = new HashSet<GameObject>();
		}

		//DEBUG
		friendHashSetDebug = friendHashSet.Count.ToString ();
		foeHashSetDebug = foeHashSet.Count.ToString ();

		friendHashSet.RemoveWhere(item => item == null);
		foeHashSet.RemoveWhere(item => item == null);

		RandomCalc ();

		PickEnemies ();




		Vector3 height3 = getHeightAvoidance ();

		Vector3 chase3 = goToTarget (target);

		Vector3 center3 = goToCenter ();

		Vector3 boid3 = getBoid ();

		Vector3 collision3 = getCollisionAvoidance ();

		desiredVelocity = (boid3 * boid + rand3 * random + chase3 * chase + center3 * center + collision3 * collisionAvoidance + height3 * groundAvoidance) / Mathf.Max(1,(boid + random + chase + center + collisionAvoidance + groundAvoidance));

		//Debug.DrawLine(transform.position, transform.position + desiredVelocity,Color.blue);

		if (target != null) {

			LockOnListener.track (target);

			
			float distanceToTarget = Vector3.Distance (target.transform.position, transform.position);
			if (distanceToTarget > minEngagementRange && distanceToTarget < engagementRange) {
				Vector3 aimforEnemy = aimAtTarget ();
				if (Vector3.Angle (aimforEnemy, desiredVelocity) < angleOfAttack || Vector3.Angle (aimforEnemy, transform.forward) < angleOfAttack/2) {
					desiredVelocity = aimforEnemy.normalized * (desiredVelocity.magnitude+1);
					//Debug.DrawLine (target.transform.position, transform.position);

				}
				//Debug.DrawLine (transform.position, transform.position + transform.forward * engagementRange,Color.red);
				//Debug.DrawLine (transform.position, transform.position + aimforEnemy * engagementRange,Color.green);
			}
		}


	}

	void PickEnemies(){
		//Get sophisticated logic here


		//TODO
		if (foeHashSet.Count > 0) {
			GameObject[] jam = new GameObject[foeHashSet.Count];
			foeHashSet.CopyTo (jam);
			target = jam [0];
		}
	}

	Vector3 getBoid(){
		Vector3 targetDirection = new Vector3 ();
		Vector3 centerOfBoidMass = new Vector3 ();
		Vector3 separation3 = new Vector3 ();
		Vector3 cohesion3 = new Vector3 ();
		foreach(GameObject go in friendHashSet){
			centerOfBoidMass += go.transform.position;
			if((transform.position - go.transform.position).sqrMagnitude < minDistance * go.GetComponent<EnemyFlier>().minDistance)
			{
				separation3 += (transform.position - go.transform.position);
			}
			cohesion3 += go.GetComponent<Rigidbody> ().velocity;
		}
		if (friendHashSet.Count > 0) {
			cohesion3 /= friendHashSet.Count;
			centerOfBoidMass /= friendHashSet.Count;
		}
		Vector3 attraction3 = centerOfBoidMass - transform.position;


		return attraction3 + cohesion3 + separation3;
	}

	Vector3 getHeightAvoidance(){
		Terrain t = Terrain.activeTerrain;
		if (t == null)
			return Vector3.zero;
		Rigidbody rb = GetComponent<Rigidbody> ();
		Vector3 calculatedPosition = rb.position + rb.velocity * 10;
		if (t.SampleHeight(calculatedPosition) + minHeight > rb.position.y) {
			return Vector3.up * 100;
		}

		return Vector3.zero;
	}

	Vector3 getCollisionAvoidance(){

		Rigidbody rb = GetComponent<Rigidbody> ();
		Ray ray = new Ray();
		ray.direction = rb.velocity;
		ray.origin = rb.position;
		RaycastHit rayHit = new RaycastHit();

		if (Physics.Raycast (ray , out rayHit , 400f)) {
			if (target != null && rayHit.collider.gameObject == target && Vector3.Distance (rayHit.collider.gameObject.transform.position, transform.position) > minEngagementRange)
				return Vector3.zero;
			else
				return rayHit.normal * 10 + (transform.position - rayHit.collider.gameObject.transform.position) ;
		}
	
		return Vector3.zero;
	}

	Vector3 goToCenter(){
		if (battleFieldCenter != null) {
			if (Vector3.Distance (battleFieldCenter.transform.position, transform.position) > maxDistance) {
				return battleFieldCenter.transform.position - transform.position;
			}
		}

		return Vector3.zero;
	}

	Vector3 aimAtTarget(){
		if (target == null)
			return Vector3.zero;
		
		Rigidbody rb = GetComponent<Rigidbody> ();

		Rigidbody tb = target.GetComponent<Rigidbody> ();

		if (tb != null) {
			//aim at the spot where they will be
			float projectileVelocity = 100;
			if(GetComponent<DroneWeapon>()!= null){
				projectileVelocity = 	GetComponent<DroneWeapon>().weapon.getMuzzleVelocity();
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
	Vector3 chase3;
	Vector3 goToTarget(GameObject target){
		chase3 = Vector3.zero;

		if (target != null) {
			chase3 = target.transform.position - transform.position;
		}

		return chase3;
	}

	void RandomCalc(){
		if (target != null)
			return;
		randTimer -= Time.deltaTime;

		if (randTimer <= 0) {
			randTimer = randInterval;

			rand3.x = Random.Range (-50, 50);
			rand3.y = Random.Range (-50, 50);
			rand3.z = Random.Range (-50, 50);


		}


	}

	void FixedUpdate(){
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.AddForce (desiredVelocity);
		rb.drag = Vector3.Dot (desiredVelocity.normalized, rb.velocity.normalized) + rb.velocity.magnitude / 100;
		if (desiredVelocity.sqrMagnitude > 0) {
			rb.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (desiredVelocity), turnSpeed * Time.deltaTime);
		}

	}


}
