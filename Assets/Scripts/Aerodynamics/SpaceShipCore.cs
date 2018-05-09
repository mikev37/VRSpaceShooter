using UnityEngine;
using System.Collections;

/*
 * This hosts all the ship's data
 */
public class SpaceShipCore : MonoBehaviour {

	public Rigidbody body;

	public PowerSystem power;

	public HeatSystem heat;

	public FuelTank fueltank;

	public Environment environment;

	[Tooltip ("Coefficient Of Drag 0-1")]public float Cdrag; 

	[Tooltip ("Coefficient Of Lift 0-1")]public float Clift; 

	[Tooltip ("Coefficient Of Stability 0-1")] public float Cstability;

	public Vector3 drag;

	public float areaDrag = 0;

	public Vector3 lift;

	public float areaLift = 0;


	public int getHullIntegrity(){
		return 100;
	}


	//public LinearMapping hover;

	//public LandingGear[] gear;

	//public WeaponMount[] weapons;

	//public Thruster[] thruster;

	//public ControlSurface[] controls;

	//public Gyroscope[] gyros;

	//public TrilinearMapping rotationControls

	//public TrilinearMapping movementControls

	// Use this for initialization
	void Start () {
		//TODO CRASH if not have all my stuff
		body = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {

		Vector3 liftVector = (transform.up * .25f + Vector3.up * .75f) ;
		/*
			Method 2: faking it with half global up and half local up
		*/
		//Calculate Lift
		areaLift = 0;
		foreach (Wing w in GetComponentsInChildren<Wing>()) {
			areaLift += w.getLift (body);
		}
		lift = Clift * (environment.atmosphericDensity * body.velocity.magnitude) / 2 * areaLift * liftVector.normalized;


		areaDrag = 0;
		foreach (Wing w in GetComponentsInChildren<Wing>()) {
			areaDrag += w.getDrag (body);
		}

		foreach (Deployable w in GetComponentsInChildren<Deployable>()) {
			areaDrag += w.getDrag ();
		}

		//Calculate Drag


		drag = Cdrag * (environment.atmosphericDensity * body.velocity.sqrMagnitude) / 2 * areaDrag * -body.velocity.normalized;

		body.angularDrag = (environment.atmosphericDensity * body.velocity.magnitude * Cstability + environment.atmosphericDensity * Cstability)/2;


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//


		//Create rotational drag based off control surfaces




		/*
		 * 
		 * //Method One: Take the current Velocityvector and rotate 90 degrees. (Don't know in what direction)
		Vector3 liftVector = body.gameObject.transform.InverseTransformDirection (body.velocity);

		Quaternion rotation = new Quaternion ();

		rotation.eulerAngles = new Vector3 (-90, 0, 0);

		liftVector = rotation * liftVector ;
		*/


		body.AddForce (lift);
		body.AddForce (drag);

	}



}
