using UnityEngine;
using System.Collections;

public class PositionBordDroit : MonoBehaviour {

	public GameObject go_camera ;

	const float _F_DISTANCEZLIMITECAMERA= 1.0f ;
	const float _F_DISTANCEXLIMITECAMERA= 20.0f ;
	float _F_RATIOXPOSITIONBORD ;

	void Start ()
	{
		go_camera = GameObject.FindGameObjectWithTag ("MainCamera") ;
		_F_RATIOXPOSITIONBORD= Screen.width / 17 * 0.5f ;
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.transform.position = new Vector3 (go_camera.transform.position.x + 5,
			go_camera.transform.position.y,
			go_camera.transform.position.z + _F_DISTANCEZLIMITECAMERA);	
	}
}
