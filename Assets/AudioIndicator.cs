using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIndicator : MonoBehaviour {

	public SwitchMapping swiMap;
	public AudioSource audio;
	public float maxVolume = 1f;
	void Start (){
		if (audio == null)
			audio = GetComponent<AudioSource> ();
	}
	// Update is called once per frame
	void Update () {
		if (swiMap != null) {
			if (audio != null) {
				if (swiMap.on)
					audio.volume = maxVolume;
				else
					audio.volume = 0;
			}
		}
	}
}
