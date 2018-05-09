using UnityEngine;
using System.Collections;

using Valve.VR.InteractionSystem;

public class SpoolUp : MonoBehaviour {

	public AudioSource audio;

	public PowerUse power;

	public AudioClip spinUp;
	public AudioClip spinDown;

	public float timer;
	public float time;

	public LinearMapping linMap;

	public SwitchMapping swiMap;
	bool internalState;
	public SwitchMapping toggle;

	// Use this for initialization
	void Start () {
		internalState = swiMap.on;
	}
	
	// Update is called once per frame
	void Update () {

		if (linMap != null) {
			if (power == null && !power.isPowered ()) {
				linMap.value = 0;
			}
			else if (swiMap.on) {
				linMap.value = (time - timer) / time;
			} else {
				linMap.value = (timer) / time;
			}
		}
		if (power == null || power.isPowered()) {
			if (timer > 0) {
				timer -= Time.deltaTime;
				if (timer <= 0) {
					toggle.on = internalState;
					timer = 0;
				}
			}
		} 
		else if (timer < time) {
				timer += Time.deltaTime;
			if(timer > time)
			{
				timer = time;
			}
		}


		if (swiMap.on != internalState) {
			if (power == null || power.isPowered ()) {
				internalState = swiMap.on;
				if (timer <= 0) {
					if (toggle.on) {
						audio.clip = spinDown;
					} else {
						audio.clip = spinUp;
					}

					audio.Play ();
					timer = time;
				}
			}
		}
	}
}
