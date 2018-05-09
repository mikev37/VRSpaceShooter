//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Basic throwable object
//
//=============================================================================

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	[RequireComponent( typeof( Rigidbody ) )]
	[RequireComponent( typeof( Joint ) )]
	public class JointAttachable : MonoBehaviour
	{
		SpringJoint joint;
		Rigidbody rb;
		//-------------------------------------------------
		void Awake()
		{
			rb = GetComponent<Rigidbody>();
			rb.maxAngularVelocity = 50.0f;
			joint = GetComponent<SpringJoint> ();
		}


		//-------------------------------------------------
		private void OnHandHoverBegin( Hand hand )
		{
			bool showHint = false;

			if ( showHint )
			{
				ControllerButtonHints.ShowButtonHint( hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger );
			}
		}


		//-------------------------------------------------
		private void OnHandHoverEnd( Hand hand )
		{
			StartCoroutine( LateDetach( hand ) );
			ControllerButtonHints.HideButtonHint( hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger );
		}


		//-------------------------------------------------
		private void HandHoverUpdate( Hand hand )
		{
			//Trigger got pressed
			if (hand.GetStandardInteractionButtonDown ()) {
				//TODO ALL MY WORK
				joint.connectedBody = hand.gameObject.GetComponentInChildren<Rigidbody> ();
				joint.spring = 500;
				joint.damper = 500;
				rb.isKinematic = false;
				ControllerButtonHints.HideButtonHint (hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
			} else if(hand.GetStandardInteractionButtonUp()) {
				StartCoroutine (LateDetach (hand));
			}
		}

		//-------------------------------------------------
		private void OnAttachedToHand( Hand hand )
		{
			
		}


		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			
		}


		//-------------------------------------------------
		private void HandAttachedUpdate( Hand hand )
		{
			//Trigger got released
			if ( !hand.GetStandardInteractionButton() )
			{
				// Detach ourselves late in the frame.
				// This is so that any vehicles the player is attached to
				// have a chance to finish updating themselves.
				// If we detach now, our position could be behind what it
				// will be at the end of the frame, and the object may appear
				// to teleport behind the hand when the player releases it.
				StartCoroutine( LateDetach( hand ) );
			}
		}


		//-------------------------------------------------
		private IEnumerator LateDetach( Hand hand )
		{
			yield return new WaitForEndOfFrame();
			//joint.connectedBody = null; //hand.gameObject.GetComponentInChildren<Rigidbody>();
			rb.isKinematic = true;
			joint.spring = 00;
			joint.damper = 00;
		}


		//-------------------------------------------------
		private void OnHandFocusAcquired( Hand hand )
		{
			
		}


		//-------------------------------------------------
		private void OnHandFocusLost( Hand hand )
		{
			
		}
	}
}
