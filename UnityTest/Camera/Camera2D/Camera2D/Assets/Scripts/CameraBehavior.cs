using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	/*** Publique ***/
	public float m_fDistanceCentreBord ; // Définit la distance entre le centre de l'écran et les bords qui seront "push" par le joueur
	public float m_fDistanceJoueurEnZ ; // Définir la distance entre la caméra et le joueur (profondeur).
	public float m_fDistanceJoueurEnY ; // Définir la distance entre la caméra et le joueur (hauteur).

	/*** Privée ***/
	GameObject go_player ;
	GameObject go_toLookAt ;

	int m_iVitesse ;

	Vector3 m_v3CentreCamera ;
	Vector3 m_v3LimiteDroite ;
	Vector3 m_v3LimiteGauche ;

	/***********************************************************/
	/*** Fonctions***/
	// Use this for initialization
	void
	Start ()
	{
		go_player= GameObject.FindGameObjectWithTag ("Player") ;
		go_toLookAt= GameObject.FindGameObjectWithTag ("toLookat") ;

		m_iVitesse= go_player.GetComponent<Controller>().getVitesse();

		initialiserPositionToLookAt ();
		initialiserPositionCamera ();
	}
	
	// Update is called once per frame
	void 
	Update ()
	{
		/******/
		// A supprimer une fois la vitesse définie après nos tests.
		if (m_iVitesse != go_player.GetComponent<Controller> ().getVitesse ())
			m_iVitesse = go_player.GetComponent<Controller> ().getVitesse ();

		m_v3CentreCamera = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + m_fDistanceJoueurEnZ);

		setSpeedupPushZoneBords ();
		speedup_push_zone () ;
		transform.LookAt(go_toLookAt.transform);
	}
		
	/*** Gestion Camera ***/
	// Initialise la position et la rotation de la caméra
	void
	initialiserPositionCamera ()
	{
		transform.position = new Vector3 (0.1f, m_fDistanceJoueurEnY, go_player.transform.position.z - m_fDistanceJoueurEnZ);
		transform.rotation = new Quaternion(0, 0, 0, 1.0f);
	}

	// Initialise la position de l'objet à "look at"
	void 
	initialiserPositionToLookAt ()
	{
		go_toLookAt.transform.position= new Vector3(go_player.transform.position.x, 
			1, 
			go_player.transform.position.z);
	}


	// Met à jour la position en Z de la camera
	public 
	void
	mettreAJourPositionCameraZ(Vector3 p_v3)
	{
		transform.position+= p_v3;
	}
	// Met à jour la position de l'objet "toLookAt" 
	void
	mettreAJourPositionToLookAt (int p_iDirection)
	{
		go_toLookAt.transform.position= new Vector3(go_toLookAt.transform.position.x + m_iVitesse * Time.deltaTime * p_iDirection, 
			go_toLookAt.transform.position.y, 
			go_toLookAt.transform.position.z);
	}

	// Met à jour les bords pour le speedup push
	void 
	setSpeedupPushZoneBords ()
	{
		m_v3LimiteDroite = m_v3CentreCamera + new Vector3 (m_fDistanceCentreBord, 0.0f, 0.0f);
		m_v3LimiteGauche = m_v3CentreCamera + new Vector3 (-m_fDistanceCentreBord, 0.0f, 0.0f);
	}


	/// une zone est définie par un bord. Le côté de droite fait bouger la camera lorsque le personnage la touche.
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
				mettreAJourPositionToLookAt (1);
				
				this.transform.position =
					new Vector3 (this.transform.position.x + m_iVitesse * Time.deltaTime, 
						m_fDistanceJoueurEnY,
						go_player.transform.position.z - m_fDistanceJoueurEnZ);

				//this.transform.LookAt (go_toLookAt.transform);
			}
		}

		if(go_player.transform.position.x < m_v3LimiteGauche.x)
		{
			if (Direction.DIRECTION_GAUCHE == directionCourrante)
			{
				mettreAJourPositionToLookAt (-1);

				this.transform.position =
					new Vector3 (this.transform.position.x - m_iVitesse * Time.deltaTime, 
						m_fDistanceJoueurEnY,
						go_player.transform.position.z - m_fDistanceJoueurEnZ);
			} 
		} 
	}

	/*** Debug ***/
	void afficheDonnees ()
	{}

}



