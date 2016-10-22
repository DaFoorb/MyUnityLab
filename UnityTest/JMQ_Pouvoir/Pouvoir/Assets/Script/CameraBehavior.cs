using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	public int posX ;
	public int posY ;
	public int posZ ;

	public GameObject go_mainCharacter ;

	void Start ()
	{

	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {

		// Déplacement en même temps que le personnage
		transform.position = new Vector3 (go_mainCharacter.GetComponent<Transform> ().position.x - posX,
			go_mainCharacter.GetComponent<Transform> ().position.y + posY,
			go_mainCharacter.GetComponent<Transform> ().position.z - posZ) ;
		// Regarde vers le main character
		transform.LookAt (go_mainCharacter.GetComponent<Transform>().position) ;
	}
}
