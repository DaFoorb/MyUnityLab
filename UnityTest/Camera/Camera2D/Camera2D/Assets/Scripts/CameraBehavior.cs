using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	/*** Publique ***/
	public int m_iVitesse;

	/*** Privée ***/
	GameObject go_player ;
	float m_fDistanceJoueurEnZ ;
	float m_fDistanceCentreBord ;
	Vector3 m_v3CentreCamera ;
	Vector3 m_v3LimiteDroite ;
	Vector3 m_v3LimiteGauche ;

	/***********************************************************/

	/*** Fonctions***/
	// Use this for initialization
	void
	Start ()
	{
		m_iVitesse = 8;
		go_player= GameObject.FindGameObjectWithTag ("Player") ;

		m_fDistanceJoueurEnZ = 15.0f;
		m_fDistanceCentreBord = 3.0f;

		initialiserPositionCamera ();
	}
	
	// Update is called once per frame
	void 
	Update ()
	{
		miseAJourSpeedupPushZoneBords ();
		m_v3CentreCamera = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + m_fDistanceJoueurEnZ);
		speedup_push_zone () ;
	}
		
	/*** Gestion Camera ***/
	// Initialise la position et la rotation de la caméra
	void
	initialiserPositionCamera ()
	{
		transform.position = new Vector3 (0.1f, 3.0f, go_player.transform.position.z - m_fDistanceJoueurEnZ);
		transform.rotation = new Quaternion(0, 0, 0, 1.0f);
	}

	void miseAJourSpeedupPushZoneBords ()
	{
		m_v3LimiteDroite = m_v3CentreCamera + new Vector3 (m_fDistanceCentreBord, 0.0f, 0.0f);
		m_v3LimiteGauche = m_v3CentreCamera + new Vector3 (-m_fDistanceCentreBord, 0.0f, 0.0f);
	}

	/// <summary>
	/// une zone est définie par un bord. Le côté de droite fait bouger la camera lorsque le personnage la touche.
	/// </summary>
	void 
	speedup_push_zone ()
	{
		// Utile pour définir le mouvement de la camera selon la position du joueur
		Direction directionCourrante = go_player.GetComponent<Player> ().getDirectionCourrante ();

		// !!! Limiter le déplacement de la caméra au bord du niveau !!!
		// Si le personnage dépasse cette limite, alors le personnage pousse la camera
		if(m_v3LimiteDroite.x < go_player.transform.position.x)
		{
			if (Direction.DIRECTION_DROITE == directionCourrante)
			{
				this.transform.position =
					new Vector3 (this.transform.position.x + m_iVitesse * Time.deltaTime, 
						this.transform.position.y,
						this.transform.position.z);
			}
			// Si le joueur se trouve au bord du niveau
			else if (Direction.DIRECTION_GAUCHE == directionCourrante)
			{
				// TO DO
			}
		}

		if(go_player.transform.position.x < m_v3LimiteGauche.x)
		{
			if (Direction.DIRECTION_GAUCHE == directionCourrante)
			{
				this.transform.position =
					new Vector3 (this.transform.position.x - m_iVitesse * Time.deltaTime, 
						this.transform.position.y,
						this.transform.position.z);
			} 
			// Si le joueur se trouve au bord du niveau
			else if (Direction.DIRECTION_DROITE == directionCourrante)
			{
				// TO DO
			}
		} 
	}

	/*** Debug ***/
	void afficheDonnees ()
	{}

}



