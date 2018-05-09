using UnityEngine;
using System.Collections;
/**
 * Purpose of the class is to separate collision into a layer separate from the AIFlier.
 * 
 * Such that AIFlier can collide with Terrain, while the Radar will not check collision with it.
 */
public class AIFlierRadar : MonoBehaviour {

	EnemyFlier flier;

	// Use this for initialization
	void Awake () {
		flier = GetComponentInParent<EnemyFlier> ();
	}

	void OnTriggerEnter(Collider col){


		if (col.gameObject == gameObject || col.isTrigger)
			return;
		if (col.tag == flier.friend) {
			flier.friendHashSet.Add (col.gameObject);
		} else if (col.tag == flier.foe) {
			flier.foeHashSet.Add (col.gameObject);
		} 
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject == gameObject || col.isTrigger)
			return;
		if (flier.friendHashSet.Contains (col.gameObject)) {
			flier.friendHashSet.Remove (col.gameObject);
		}
		if (flier.foeHashSet.Contains (col.gameObject)) {
			flier.foeHashSet.Remove (col.gameObject);
		}

	}


}
