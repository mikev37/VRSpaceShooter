using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/*
 * This system is used by the HUD to display the known entities
 */
public class TrackingRadar : MonoBehaviour {

	public List<GameObject> targetList;
	public GameObject tracker;
	public Radar radar;

	public Sprite passive,neutral,enemy,friendly;



	// Use this for initialization
	protected virtual void Start () {
		if (radar == null) {
			radar = GetComponentInParent <Radar> ();
		}
		targetList = new List<GameObject> ();
	}

	public virtual void positionTracker(Transform tracker, Transform actual){
		tracker.position =actual.position;

	}

	public virtual void setImage(Image trackImage,GameObject actual){
		switch (IFFMethods.classify(actual)) {
		case IFFClass.FRIENDLY:
			trackImage.sprite = friendly;
			break;
		case IFFClass.ENEMY:
			trackImage.sprite = enemy;
			break;
		case IFFClass.NEUTRAL:
			trackImage.sprite = neutral;
			break;
		case IFFClass.PASSIVE:
			trackImage.sprite = passive;
			break;

		}
	}
	
	// Update is called once per frame
	void Update () {
		while (radar.targetList.Count < targetList.Count) {
			Destroy (targetList [0]);
			targetList.RemoveAt (0);
		}
		while(radar.targetList.Count > targetList.Count){
			GameObject addition = (GameObject)Instantiate (tracker, Vector3.zero, this.transform.rotation,this.transform);
			targetList.Add(addition);
		}
		for (int i = 0; i < targetList.Count; i++) {
			
			positionTracker (targetList [i].transform, radar.targetList [i]);

			setImage(targetList [i].GetComponentInChildren<Image> (),radar.targetList [i].gameObject);

		}
	}
}