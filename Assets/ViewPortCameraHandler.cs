using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPortCameraHandler : MonoBehaviour {

	public Camera[] cameras;

	public string[] labels;

	public Text labelfield;

	int current = 0;

	public void next(){
		cameras [current].gameObject.SetActive (false);
		current++;
		if (current >= cameras.Length) {
			current = 0;
		}
		cameras [current].gameObject.SetActive (true);
		if (labelfield != null) {
			labelfield.text = labels [current];
		}
	}

	public void prev(){
		cameras [current].gameObject.SetActive (false);
		current--;
		if (current < 0) {
			current = cameras.Length - 1;
		}
		cameras [current].gameObject.SetActive (true);
		if (labelfield != null) {
			labelfield.text = labels [current];
		}
	}
}
