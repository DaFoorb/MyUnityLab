using UnityEngine;
using System.Collections;

public enum Direction
{
	DIRECTION_INDEFINIE,
	DIRECTION_DROITE, 
	DIRECTION_GAUCHE
} ;

public class Player : MonoBehaviour {

	/*** Variables membres ***/
	Direction m_eDirectionCourrante ;

	/*** Fonctions ***/
	void Start()
	{
		m_eDirectionCourrante = Direction.DIRECTION_INDEFINIE;
	}
		
	/*** Getters/Setters ***/

	// Met à jour la direction du personnage
	public
	void
	setDirectionCourrante(Direction p_nouvelleDirection)
	{
		m_eDirectionCourrante = p_nouvelleDirection;
	}

	// Récupère la direction courrante du personnage
	public
	Direction
	getDirectionCourrante()
	{
		return m_eDirectionCourrante;
	}
}
