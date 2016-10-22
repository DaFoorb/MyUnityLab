using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	public int posX ;
	public int posY ;
	public int posZ ;

	int i_mode ;

	float f_distance ;
	float f_ratio ;

	public GameObject go_mainCharacter ;
	public GameObject go_level ;
	//public Text t_text ;

	// Use this for initialization
	void Start () {
		f_distance= go_level.transform.localScale.x + 30.0f ;

		f_ratio= f_distance/go_level.transform.localScale.x ;
	}
	
	// Update is called once per frame
	void Update () {
		cameraMode ();
		changeCameraMode ();
		// Regarde vers le main character
		transform.LookAt (go_mainCharacter.transform.position) ;
	}


	void changeCameraMode()
	{
		if (Input.GetKey("1")) 
		{
			i_mode= 1;
		}

		if (Input.GetKey("2")) 
		{
			i_mode= 2;
		}
	}

	void cameraMode () 
	{
		switch (i_mode)
		{
		case 1:
			// Toujours derrière l'objet
			transform.position = new Vector3 (
				go_mainCharacter.transform.position.x - posX,
				go_mainCharacter.transform.position.y + posY,
				go_mainCharacter.transform.position.z - posZ
			);

			//t_text.text = "Camera mode 1 : Follow behind";

			break ;
		case 2:
			// Tourne autour de l'objet en fonction de la taille du niveau
			transform.position = new Vector3 (
				(go_mainCharacter.transform.position.x - posX) * f_ratio,
				go_mainCharacter.transform.position.y + posY,
				go_mainCharacter.transform.position.z - posZ
			);
			//t_text.text = "Camera mode 2 : Turn around";
			break ;
		case 3:
			break ;
		}
	}
}
