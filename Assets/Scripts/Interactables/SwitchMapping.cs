using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SwitchMapping : MonoBehaviour {
	public bool on;

	public UnityEvent eventUnity;

	void Update(){
		if (eventUnity != null && on) {
			eventUnity.Invoke ();
		}
	}
}
