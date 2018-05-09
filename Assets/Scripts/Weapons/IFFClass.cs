using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IFFClass{
	FRIENDLY,
	ENEMY,
	PASSIVE,
	NEUTRAL,
	PROJECTILE
}

static public class IFFMethods{
	public static IFFClass classify(GameObject go){
		if (go.tag.Contains ("Blue")) {
			return IFFClass.FRIENDLY;
		} else if (go.tag.Contains ("Red")) {
			return IFFClass.ENEMY;
		}else if (go.GetComponent<Rigidbody> ().velocity.sqrMagnitude > 0) {
			return IFFClass.NEUTRAL;
		} else {
			return IFFClass.PASSIVE;
		}
		//TODO ADD support for projectiles
	}
}
