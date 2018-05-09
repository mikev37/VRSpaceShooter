using UnityEngine;
using System.Collections;

public class EnemyTarget : MonoBehaviour {

	public GameObject displayPlaneObject;
	public GameObject baseObject;
	public GameObject baseCam;


	Collider displayPlane;

	// Use this for initialization
	void Start () {
		displayPlane = displayPlaneObject.GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetMouseButtonDown(0)) {
			//Debug.Log ("HEY THERE LITTLE MAN");
			//Debug.Log(displayPlane);
			Ray ray = new Ray();
			ray.direction = baseObject.transform.position - baseCam.transform.position;
			ray.origin = baseCam.transform.position;
			RaycastHit rayDistance;


			if (displayPlane.Raycast (ray, out rayDistance,200))
				transform.position = ray.GetPoint (rayDistance.distance);
			else
				transform.position = ray.origin;

		transform.LookAt(Camera.main.transform.position, -baseCam.transform.up);
			//Debug.Log (ray.direction);

			//Debug.DrawRay (ray.origin,ray.direction * 40,Color.green,30,false);
		//}


	}
}
