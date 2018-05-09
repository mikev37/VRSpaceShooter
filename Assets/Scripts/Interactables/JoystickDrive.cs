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
	public class JoystickDrive : MonoBehaviour
	{
		public Transform startRotation;
		public Vector3 rotateOffset = new Vector3(90,0,0);
		public JoyStickMapping joystickMapping;
		public float maxDeflection;
		protected Transform handRotation;

		//-------------------------------------------------
		void Awake()
		{

		}


		//-------------------------------------------------
		void Start()
		{
			if ( joystickMapping == null )
			{
				joystickMapping = GetComponent<JoyStickMapping>();
			}
		}


		//-------------------------------------------------
		protected virtual void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() )
			{
				hand.HoverLock( GetComponent<Interactable>() );
				handRotation = hand.transform;
			}

			if ( hand.GetStandardInteractionButtonUp() )
			{
				hand.HoverUnlock( GetComponent<Interactable>() );
				handRotation = null;

			}
				
		}

		float angleDistanceZero(float input){
			float angle = input;
			if (input > 180)
				angle -= 360;

			return angle;
		}


		protected virtual void Update()
		{

			if (handRotation != null) {

				bool stop = false;

				Quaternion turnedRotation = handRotation.rotation * Quaternion.Euler (rotateOffset);

				if (startRotation != null && maxDeflection > 0) {
					float angleToStart = Quaternion.Angle (transform.rotation, startRotation.rotation);

					float angleToHand = Quaternion.Angle (startRotation.rotation, turnedRotation);

					if (angleToStart >= maxDeflection && angleToHand >= maxDeflection) {
						stop = true;
					}

				}

				if (!stop) {


					transform.rotation = Quaternion.Lerp (transform.rotation, turnedRotation, .1f);//handRotation.rotation;

				}
			} else if(startRotation != null){
				transform.rotation = Quaternion.Lerp (transform.rotation, startRotation.rotation, .1f);//Vector3.Lerp( startRotation.position, endPosition.position, linearMapping.value );
			}

			joystickMapping.pitch = angleDistanceZero(transform.localRotation.eulerAngles.x);
			joystickMapping.rotation = angleDistanceZero(transform.localRotation.eulerAngles.z);
			joystickMapping.yaw = angleDistanceZero(transform.localRotation.eulerAngles.y);

		}
	}
}
