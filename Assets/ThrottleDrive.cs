using UnityEngine;
using System.Collections;

using Valve.VR.InteractionSystem;

public class ThrottleDrive : LinearDrive {

	public TrilinearMapping triMap;

	//-------------------------------------------------
	protected virtual void HandHoverUpdate( Hand hand )
	{
		if (hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip)  )
		{
			hand.HoverLock( GetComponent<Interactable>() );

			initialMappingOffset = linearMapping.value - CalculateLinearMapping( hand.transform );
			sampleCount = 0;
			held = true;
			mappingChangeRate = 0.0f;
		}

		else if (hand.controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_Grip)  )
		{
			held = false;
			hand.HoverUnlock( GetComponent<Interactable>() );

			CalculateMappingChangeRate();
		}


		if (hand.controller.GetTouch (Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad)) {
			Vector2 updown = hand.controller.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0);

			triMap.value.y = updown.y;

			triMap.value.x = updown.x;
		} else {
			triMap.value.y = 0;

			triMap.value.x = 0;
		}
		if ( held )
		{
			UpdateLinearMapping( hand.transform );
		}

		triMap.value.z = linearMapping.value * 2 - 1;


	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}
}
