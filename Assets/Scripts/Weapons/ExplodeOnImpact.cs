using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Utility;


/*
 * If enabled, will explode on proximity
 */
namespace UnityStandardAssets.Effects
{
	public class ExplodeOnImpact : MonoBehaviour
	{
		public Transform explosionPrefab;
		public float detonationImpactVelocity = 10;
		public float sizeMultiplier = 1;

		// implementing one method from monobehviour to ensure that the enable/disable tickbox appears in the inspector
		private void Start()
		{
		}


		private void OnCollisionEnter(Collision col)
		{
			if (this.enabled) {
				Instantiate (explosionPrefab, col.contacts [0].point,
					Quaternion.LookRotation (col.contacts [0].normal));

				Destroy (this.gameObject);


			}
		}
			
	}
}
