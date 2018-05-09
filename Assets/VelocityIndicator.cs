using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VelocityIndicator : MonoBehaviour {

	Rigidbody self;
	public Rigidbody relative;
	Image myImage;
	public int multiplier;
	public float alphaCutOff;
	public float alphaMax;

	// Use this for initialization
	void Start () {
		self = GetComponentInParent<SpaceShipCore> ().GetComponent<Rigidbody> ();
		myImage = GetComponentInChildren<Image> ();
	}

	void setAlpha(Vector3 measurable){
		Color col = myImage.color;
		float magnitude = measurable.magnitude;
		if (magnitude < alphaCutOff) {
			col.a = 0;
		} else if (magnitude > alphaMax) {
			col.a = 1f;
		} else {
			col.a = (magnitude - alphaCutOff) / (alphaMax - alphaCutOff);
		}
		myImage.color = col;
	}
	
	// Update is called once per frame
	void Update () {
		if (relative == null) {
			Vector3 measurable = self.velocity;
			Vector3 offset = measurable.normalized * multiplier;
			this.gameObject.transform.position = self.position + offset;
			setAlpha (measurable);

		} else {
			Vector3 offset = (self.velocity - relative.velocity).normalized * multiplier;
			this.gameObject.transform.position = self.position + offset;
			setAlpha (self.velocity - relative.velocity);
		}
	}
}
