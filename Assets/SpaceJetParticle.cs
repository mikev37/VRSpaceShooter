using UnityEngine;
using System.Collections;
[RequireComponent(typeof(ParticleSystem))]
public class SpaceJetParticle : MonoBehaviour {

	ParticleSystem ps;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem> ();
		rb = GetComponentInParent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		ps.startLifetime = 4 * Environment.Atmosphere;
		ps.startSize = .0001f * rb.velocity.magnitude;
	}
}
