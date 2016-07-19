using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCameraPositionEvent : MonoBehaviour {

	public int i_firstCamera ;
	public int i_secondCamera ;

	int i_Id ;

	public GameObject main_camera ;

	bool b_isOnTrigger ;

	int i_cameraPositionCurrent ;

	void Start () {

		i_Id = i_firstCamera;

		i_cameraPositionCurrent= i_firstCamera ;
		b_isOnTrigger= false ;

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerStay (Collider collisionInfo)
	{
		// On change la position/rotation de la camera
		// Verifier la direction du personnage !!
		Debug.Log("OnCollisionEnter CameraPositionEvent") ;

		if ("Player" == collisionInfo.tag
		    && !b_isOnTrigger)
		{
			b_isOnTrigger = true;

			Dictionary<int, KeyValuePair<Vector3, Quaternion>> list_cameras = main_camera.GetComponent<ListCamera> ().getListCameras () ;

			if (i_firstCamera == i_cameraPositionCurrent)
			{
				i_cameraPositionCurrent= i_secondCamera ;

				foreach (KeyValuePair<int, KeyValuePair<Vector3, Quaternion>> pair in list_cameras)
				{
					if (i_secondCamera == pair.Key)
					{
						main_camera.transform.position= pair.Value.Key;
						main_camera.transform.rotation= pair.Value.Value;
					}
				}
			} 
			else 
			{
				i_cameraPositionCurrent= i_firstCamera ;

				foreach (KeyValuePair<int, KeyValuePair<Vector3, Quaternion>> pair in list_cameras)
				{
					if (i_firstCamera == pair.Key)
					{
						main_camera.transform.position= pair.Value.Key;
						main_camera.transform.rotation= pair.Value.Value;
					}
				}
			}

		}

	}	



	/// <summary>
	/// Raises the trigger exit event.
	/// </summary>
	/// <param name="collisionInfo">Collision info.</param>
	void OnTriggerExit (Collider collisionInfo)
	{
		b_isOnTrigger = false;
	}



//	public void setCurrentCameraPosition (int p_if_Id)
//	{
//		i_cameraCurrent= p_if_Id ;
//	}
}



