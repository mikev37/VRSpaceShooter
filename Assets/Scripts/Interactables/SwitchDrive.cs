//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Drives a linear mapping based on position between 2 positions
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class SwitchDrive : LinearDrive
	{
		
		public SwitchMapping switchMapping;


		//-------------------------------------------------
		void Start()
		{
			base.Start ();
			if (switchMapping == null) {
				switchMapping = GetComponent<SwitchMapping>();
			}
		}


		//-------------------------------------------------
		protected override void UpdateLinearMapping( Transform tr )
		{

			base.UpdateLinearMapping (tr);

			Debug.Log ("new value will be : " + (linearMapping.value > 0.5f) as string);
			switchMapping.on = linearMapping.value > 0.5f;

		}


		//-------------------------------------------------
		protected override void Update()
		{
			if (!base.held) {
				if (switchMapping.on) {
					transform.position = Vector3.Lerp (transform.position, endPosition.position, .1f);
				} else {
					transform.position = Vector3.Lerp (transform.position, startPosition.position, .1f);
				}
			} else {
				base.Update ();
			}
		}
	}
}
