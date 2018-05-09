using UnityEngine;
using System.Collections;
/*
 * Can play an animation and create drag
 */
public class Deployable : MonoBehaviour {

	public Animator deployAnim;
	public AnimationClip animClip;
	public SwitchMapping deployed;
	public bool internalState;
	public float deployedDrag = 0;

	// Use this for initialization
	void Start () {
		RuntimeAnimatorController myController = deployAnim.runtimeAnimatorController;

		AnimatorOverrideController myAnimatorOverride = new AnimatorOverrideController();
		myAnimatorOverride.runtimeAnimatorController = myController;

		myAnimatorOverride ["DeployPanel"] = animClip;	

		deployAnim.runtimeAnimatorController = myAnimatorOverride;

		internalState = deployed.on;


	}

	public float getDrag(){
		if (deployed.on) {
			return deployedDrag;
		} else
			return 0;
	}

	// Update is called once per frame
	void Update () {
		if (deployed.on != internalState) {
			deployAnim.SetBool ("deployed", deployed.on);
			internalState = deployed.on;
		}
	}
}
