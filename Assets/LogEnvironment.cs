using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogEnvironment : MonoBehaviour {
	public Text textfield;
	public Image image;
	Rigidbody spaceRb;
	public Renderer render;
	bool setImages = false;

	// Use this for initialization
	void Start () {
		if (textfield == null) {
			textfield = GetComponent<Text> ();
		}
		if (image == null) {
			image = GetComponent<Image> ();
		}


		spaceRb = GetComponentInParent<SpaceShipCore> ().GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		textfield.text = 
			"Orbital Body: " + Environment.Name + "\n\n";
		if (Environment.OrbitHeight > 0) {
			textfield.text += "Apoapsis: " + Environment.OrbitHeight + spaceRb.velocity.magnitude + "\n"
				+ "Pereapsis: " + (Environment.OrbitHeight - spaceRb.velocity.magnitude) + "\n"
				+ "Orbital Velocity: " + (Environment.Orbit + spaceRb.velocity).magnitude + "\n";
		} else {
			textfield.text += "Apoapsis: N/A \n"
				+ "Pereapsis: N/A \n"
				+ "Orbital Velocity: N/A \n\n";
		}
		textfield.text += "Percieved Gravity: " + (Environment.Gravity.magnitude / 9.8f) + " G \n\n";

		if (Environment.Atmosphere > 0) {
			textfield.text += "Atmospheric Pressure: " + (Environment.Atmosphere * 101.3f) + " kPs (" + Environment.Atmosphere + " A) \n";
			textfield.text += "Wind Speed: " + (Environment.WeatherActivity.magnitude) + "m/s \n";
			textfield.text += "Atmospheric Acidity: " + (Environment.AtmosphereAcidity) + " Ph \n";
			textfield.text += "Atmospheric Humidity: " + (Environment.Humidity * 100) + " % \n";
		} else {
			textfield.text += "Atmospheric Pressure: " + (Environment.Atmosphere * 101.3f) + " kPs (" + Environment.Atmosphere + " A) \n";
			textfield.text += "Wind Speed: N/A \n\n";
			textfield.text += "Atmospheric Acidity: N/A Ph \n";
			textfield.text += "Atmospheric Humidity: N/A % \n";
		}
		if (!setImages) {
			if (image != null && Environment.Img != null) {
				image.sprite = Environment.Img;
			}

			if (render != null) {
				render.material.mainTexture = Environment.Texture;
			}
			setImages = true;
		}

	}
}
