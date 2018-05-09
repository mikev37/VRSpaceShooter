using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class TargetPodHandler : MonoBehaviour {

	public Vector2 cameraFov;

	public LinearMapping cameraFovMapping;

	public Transform cameraDefaultLookAt;

	public GameObject targetIndicator;

	public GameObject viewIndicator;

	public int maxCameraAngle;

	public Camera camera;

	public TrackingType typeOfTrack;

	public LockOn linkedLockOn;

	public Vector3 LookDirection;

	public Text gimballLockText;

	public Text infoText;

	private Vector3 LookAtWorldPos;

	public Transform getTarget(){
		if (gimballLockText.enabled || typeOfTrack != TrackingType.LOCK) {
			return null;
		} else {
			return targetIndicator.transform;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
		
	public void setTrackType(int trackNum){
		if (typeOfTrack == TrackingType.FWD) {
			int layerMask = 1 << 12;
			Ray ray = new Ray (camera.transform.position, camera.transform.forward);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 1000,layerMask)) {
				LookAtWorldPos = hit.point;
			} else {
				LookAtWorldPos = camera.transform.position + camera.transform.forward * 1500;
			}
		}
		typeOfTrack = (TrackingType)trackNum;
	}
	
	// Update is called once per frame
	void Update () {
		if (linkedLockOn != null) {
			linkedLockOn.refreshCamera (this);
		}
		camera.fieldOfView = cameraFov.x + cameraFovMapping.value * (cameraFov.y - cameraFov.x);
		Vector3 FwdDirection = cameraDefaultLookAt.position - camera.transform.position;
		switch (typeOfTrack) {
		case TrackingType.HEAD:
				//Cast a Ray from the camera 
			Transform cam = Camera.main.transform;
			Ray ray = new Ray (cam.position, cam.forward);
			int layerMask = 1 << 12;
				RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 1000,layerMask)) {
					LookDirection = hit.point - camera.transform.position;	
					LookAtWorldPos = hit.point;
				} else {
					LookAtWorldPos = camera.transform.position + Camera.main.transform.forward * 1500;
					LookDirection = Camera.main.transform.forward;
				}
				//Look at end point of the Ray
				break;
			case TrackingType.TRK:
				if (linkedLockOn != null) {
					if (linkedLockOn.tracking != null) {
						LookDirection = linkedLockOn.tracking.position - camera.transform.position;
						LookAtWorldPos = linkedLockOn.tracking.position;
					}
				}
				break;
		case TrackingType.LOCK:
			LookDirection = LookAtWorldPos - camera.transform.position;
			break;
			default:
				LookDirection = FwdDirection;
				break;
		}

		if (Vector3.Angle (FwdDirection, LookDirection) < maxCameraAngle) {
			gimballLockText.enabled = false;
			camera.transform.rotation = Quaternion.Slerp (camera.transform.rotation, Quaternion.LookRotation (LookDirection,transform.up), .1f);
		} else {
			gimballLockText.enabled = true;
		}
		viewIndicator.transform.position = transform.position + camera.transform.forward.normalized * 500;
		targetIndicator.transform.position = transform.position + LookDirection.normalized * 500;

		Debug.DrawRay (camera.transform.position, LookDirection, Color.blue, .1f, false);
		infoText.text = "TODO DATA\n" + Vector3.Angle (FwdDirection, LookDirection);

	}


}

public enum TrackingType{
	FWD,
	HEAD,
	TRK,
	LOCK
}