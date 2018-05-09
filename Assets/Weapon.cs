using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

	public string name;

	public abstract void fire ();

	public abstract float getMuzzleVelocity ();

	public abstract int mass ();

	public string getName (){
		return name;
	}

	public abstract void reload ();

	public abstract void arm (bool armed);

	public abstract bool isArmed ();

	public virtual bool hasAmmo ()
	{
		return ammo () > 0;
	}

	public abstract bool salvo ();

	public abstract int ammo ();

	public abstract int maxAmmo ();

	public abstract void eject ();

	public abstract void attach ();

	public Transform target;

	// Use this for initialization
	protected virtual void Start () {
	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}
}
