using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WeaponsSystem : MonoBehaviour {


	public Weapon[][] arsenal;

	public Transform target;

	public SwitchMapping masterArmSwitch;

	public bool armed(){
		if (masterArmSwitch != null) {
			return masterArmSwitch.on;
		}
		return true;
	}

	public int currentSelection;

	public void switchWeapon(){
		currentSelection++;
		if (currentSelection >= arsenal.Length) {
			currentSelection = 0;
		}

	}

	public string getName(){
		return arsenal [currentSelection] [0].getName ();
	}

	public int getAmmo(){
		int ammo = 0;
		foreach (Weapon w in arsenal[currentSelection]) {
			ammo += w.ammo ();
		}
		return ammo;
	}

	public void fire(){
		if (arsenal [currentSelection] [0].salvo ()) {
			foreach (Weapon w in arsenal[currentSelection]) {
				w.fire ();
			}
		} else {
			foreach (Weapon w in arsenal[currentSelection]) {
				if (w.hasAmmo ()) {
					w.fire ();
					break;
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		Weapon[] available = GetComponentsInChildren<Weapon> ();
		Debug.Log (available.Length);
		Dictionary<string,List<Weapon>> sorted = new Dictionary<string,List<Weapon>> ();
		foreach(Weapon w in available){
			if(sorted.ContainsKey(w.getName())){
				sorted[w.getName()].Add(w);
			}else{
				sorted[w.getName()] = new List<Weapon>();
				sorted[w.getName()].Add(w);
			}
		}
		Debug.Log (sorted.ToString());
		arsenal = new Weapon[sorted.Keys.Count][];
		int iterator = 0;
		foreach(string s in sorted.Keys){
			arsenal [iterator] = new Weapon [sorted [s].Count];
			int jaterator = 0;
			foreach (Weapon w in sorted [s]) {
				arsenal [iterator] [jaterator] = w;
				jaterator++;
			}
			iterator++;
		}
		foreach (Weapon[] wl in arsenal) {
			foreach (Weapon w in wl) {
				Debug.Log (w.getName ());
			}
		}
		Debug.Log (arsenal);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab)){
			switchWeapon ();
		}
		if (Input.GetKey (KeyCode.Space)) {
			fire ();
		}
		foreach (Weapon w in arsenal[currentSelection]) {
			w.target = target;
		}
	}
}
