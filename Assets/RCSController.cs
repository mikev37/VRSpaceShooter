using UnityEngine;
using System.Collections;

using Valve.VR.InteractionSystem;
/*
 * Centralized controller for the Reaction control system thrusters
 */
public class RCSController : MonoBehaviour {



	public SwitchMapping drag;

	public SwitchMapping thrustAssist;

	//public SwitchMapping rotation;

	public SwitchMapping antigrav;

	public LinearMapping powerMult;

	public int maxLimiter = 300;

	RCSThruster[] thrusters;

	// Use this for initialization
	void Start () {
		thrusters = GetComponentsInChildren<RCSThruster> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (RCSThruster rcs in thrusters) {
			rcs.induceDrag = drag.on;
			//rcs.decreaseRot = rotation.on;
			rcs.antiGrav = antigrav.on;
			rcs.thrustAssit = thrustAssist.on;
			//rcs.thrusterNum = thrusters.Length;
			if (powerMult != null) {
				rcs.limiter = powerMult.value * maxLimiter;
			}
		}
	}
}
