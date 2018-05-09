using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchSceen : MonoBehaviour {

	public Text data;

	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			Debug.DrawRay(contact.point, contact.normal, Color.white,30);
		}

		ContactPoint cp = collision.contacts [0];
		data.text = "Touched: " + this.transform.InverseTransformPoint(cp.point).ToString();
	}
}
