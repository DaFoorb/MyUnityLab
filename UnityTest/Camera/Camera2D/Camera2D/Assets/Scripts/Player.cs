using UnityEngine;
using System.Collections;

// [RequireComponent(typeof(Controller2D))]

public enum Direction {
	DIRECTION_INDEFINIE,
	DIRECTION_DROITE, 
	DIRECTION_GAUCHE
} ;

public class Player : MonoBehaviour {

	Direction m_eDirectionCourrante ;

	void Start()
	{
		m_eDirectionCourrante = Direction.DIRECTION_INDEFINIE;
	}
		
	/*** Getters/Setters ***/
	public
	void
	setDirectionCourrante(Direction p_nouvelleDirection)
	{
		m_eDirectionCourrante = p_nouvelleDirection;
	}

	public
	Direction
	getDirectionCourrante()
	{
		return m_eDirectionCourrante;
	}

	/*** Controle ***/
//	void InputPC()
//	{
//		if (Input.GetKey("left"))
//		{
//			GetComponent<Rigidbody>().AddForce (-Vector3.forward);
//		}
//		else if (Input.GetKey("right"))
//		{
//			GetComponent<Rigidbody>().AddForce (Vector3.forward);
//		}
//	}

}
