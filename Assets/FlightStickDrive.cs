using UnityEngine;
using UnityEngine.Events;
using System.Collections;

using Valve.VR.InteractionSystem;

public class FlightStickDrive : JoystickDrive {
	

		public TrilinearMapping triMap;

		bool held;

		public UnityEvent triggerPull,menuButton,bigButton;

		//-------------------------------------------------
		protected override void HandHoverUpdate( Hand hand )
		{
			if (!held && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip) )
			{
				hand.HoverLock( GetComponent<Interactable>() );
				held = true;
				handRotation = hand.transform;
			}else if (held && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip)  )
			{
				held = false;
				hand.HoverUnlock( GetComponent<Interactable>() );
				handRotation = null;
			}
			
			if ( held && hand.GetStandardInteractionButton()){
				triggerPull.Invoke ();
			}

			if ( held && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu)){
				menuButton.Invoke ();
			}

			if ( held && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad)){
				bigButton.Invoke ();
			}
		}

		// Update is called once per frame
		void Update () {
			base.Update ();
		}


}
