using UnityEngine;
using UnityEngine.Events;
using System.Collections;
/*
 * This will absorb damage and do things once destroyed
 */
namespace JackPotGames.VRSpaceFlyer{

	public class DamageSensor : MonoBehaviour {

		public float health = 100;

		public float maxHealth { get; private set;}

		public UnityEvent onDestroyed;

		void Start(){
			maxHealth = health;
		}

		public void Damage(float damage){
			health -= damage ;
		}

		void Update(){
			if (health <= 0) {
				if (onDestroyed != null) {
					onDestroyed.Invoke ();
				}
				enabled = false;
			}
		}
	}

}