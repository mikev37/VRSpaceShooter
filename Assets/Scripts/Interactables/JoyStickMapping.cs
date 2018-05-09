//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: A linear mapping value that is used by other components
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class JoyStickMapping : MonoBehaviour
	{
		public float pitch;
		public float yaw;
		public float rotation;
		public float deadzone = 2;
		/*void Update(){
			if (pitch < deadzone) {
				pitch = 0;
			}
			if (rotation < deadzone) {
				rotation = 0;
			}

			if (yaw < deadzone) {
				yaw = 0;
			}
		}*/
	}
}
