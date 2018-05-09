using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/**
 * Planetary variables that afect flight
 */
public class Environment : MonoBehaviour {

	public string name;
	public int orbitHeight;
	public Vector3 orbit;
	public Sprite img;
	public Vector3 gravity;
	public float atmosphericDensity;
	public float temperature;
	public float acidity;
	public Vector3 weather;
	public float humidity;
	public Texture texture;


	public static string Name;
	public static Sprite Img;
	public static int OrbitHeight;
	public static Vector3 Orbit;
	public static Vector3 Gravity;
	public static float Atmosphere;
	public static float Temperature;
	public static float AtmosphereAcidity;
	public static Vector3 WeatherActivity;
	public static float Humidity;
	public static Texture Texture;

	// Use this for initialization
	void Start () {
		Physics.gravity = gravity;
		Physics.autoSyncTransforms = false;
		Name = name;
		Img = img;
		Orbit = orbit;
		OrbitHeight = orbitHeight;
		Temperature = temperature;
		Atmosphere = atmosphericDensity;
		Gravity = gravity;
		AtmosphereAcidity = acidity;
		WeatherActivity = weather;
		Humidity = humidity;
		Texture = texture;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
