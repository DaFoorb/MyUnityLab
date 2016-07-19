using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListCamera : MonoBehaviour {

	static Dictionary<int, KeyValuePair<Vector3, Quaternion>> list_cameras= new Dictionary<int, KeyValuePair<Vector3, Quaternion>> () ;
	// Use this for initialization
	void Start () {
	// On initialise le tableau

		list_cameras.Add (0, 
						  new KeyValuePair <Vector3, Quaternion> (new Vector3 (3.25f, 4.3f, -14.0f), 
																  Quaternion.Euler (25.0f, 288.0f, 2.18f))
		) ;

		list_cameras.Add (1, 
						  new KeyValuePair <Vector3, Quaternion> (new Vector3 (-3.25f, 4.3f, -14.0f), 
																  Quaternion.Euler (10.0f, 55.0f, -1.0f))
		) ;

		list_cameras.Add (2, 
						  new KeyValuePair <Vector3, Quaternion> (new Vector3 (12.5f, 2.6f, -12.0f), 
						  										  Quaternion.Euler (23.0f, 0.4f, 0.0f))
		) ;

		foreach (KeyValuePair<int, KeyValuePair<Vector3, Quaternion>> pair in list_cameras)
		{
			if (0 == pair.Key)
			{
				transform.position= pair.Value.Key;
				transform.rotation= pair.Value.Value;
				break;
			}
		}
	}

	public
	Dictionary<int, KeyValuePair<Vector3, Quaternion>>
	getListCameras ()
	{
		return list_cameras ;
	}



	// Update is called once per frame
	void Update () {
	
	}
}
