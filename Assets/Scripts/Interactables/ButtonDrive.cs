using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	[RequireComponent( typeof ( Animation) ) ]
	public class ButtonDrive : MonoBehaviour
	{
		Animation buttonPressAnimation;
		public SwitchMapping switchMapping;
		public float delay;
		float counter;

		public UnityEvent eventOnPress;

		//-------------------------------------------------
		void Awake()
		{
			if (switchMapping == null) {
				switchMapping = GetComponent<SwitchMapping>();
			}
		}


		//-------------------------------------------------
		public void Start()
		{
			buttonPressAnimation = GetComponent<Animation> ();
		}


		//-------------------------------------------------
		private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() )
			{
				buttonPressAnimation.Play ();
				switchMapping.on = true;
				counter = delay;
				eventOnPress.Invoke ();
			}
		}
			

		public void Update()
		{
			if (counter > 0) {
				counter -= Time.deltaTime;
			}

			if (counter <= 0) {
				switchMapping.on = false;
				counter = 0;
			}
		}
	}
}
