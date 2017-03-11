using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraBehavior2 : MonoBehaviour {

	/*** Publique ***/
	public float m_fDistanceCentreBord ; // Définit la distance entre le centre de l'écran et les bords qui seront "push" par le joueur
	public float m_fDistanceJoueurEnY ; // Définir la distance entre la caméra et le joueur (hauteur).
	public float m_fDistanceJoueurEnZ ; // Définir la distance entre la caméra et le joueur (profondeur).
	//public float m_fRotationJoueurEnX ; // Définir la rotation entre la caméra et le joueur (parallèle).

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

		initialiserPositionCamera ();
		initialiserPositionToLookAt ();
		transform.LookAt(go_toLookAt.transform);
	}
	
	// Update is called once per frame
	void 
	Update ()
	{
		/******/
		// A supprimer une fois la vitesse définie après nos tests.
		if (m_iVitesse != go_player.GetComponent<Controller> ().getVitesse ())
			m_iVitesse = go_player.GetComponent<Controller> ().getVitesse ();

//		transform.rotation = new Quaternion (transform.rotation.x + m_fRotationJoueurEnX,
//			transform.rotation.y,
//			transform.rotation.z, 1);

		setSpeedupPushZoneBords ();
		speedup_push_zone_ext () ;

		//afficheDonnees ();
	}
		
	/*** Gestion Camera ***/
	// Initialise la position et la rotation de la caméra
	void
	initialiserPositionCamera ()
	{
		transform.position = new Vector3 (0.1f, 
			m_fDistanceJoueurEnY, 
			go_player.transform.position.z - m_fDistanceJoueurEnZ);
		transform.rotation = new Quaternion(0, 0, 0, 1.0f);

		m_v3CentreCamera = new Vector3 (transform.position.x,
			transform.position.y,
			go_player.transform.position.z);
	}

	// Initialise la position de l'objet à "look at"
	void 
	initialiserPositionToLookAt ()
	{
		go_toLookAt.transform.position= m_v3CentreCamera ;
	}

	// Met à jour la position de la camera
	public 
	void
	mettreAJourPositionCamera(Vector3 p_v3)
	{
		transform.position+= p_v3;
	}

	void
	mettreAJourPositionCentreCamera()
	{
		m_v3CentreCamera = new Vector3 (
			this.transform.position.x,
			this.transform.position.y,
			this.transform.position.z - m_fDistanceJoueurEnZ); 
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
	speedup_push_zone_ext ()
	{
		// Camera type Klonoa
		// Utile pour définir le mouvement de la camera selon la position du joueur
		Direction directionCourrante = go_player.GetComponent<Player> ().getDirectionCourrante ();
		Vector3 deplacement = Vector3.zero;
		int vitesseDeplacement = 0;

		// 1. Si le joueur se trouve derrière le bord gauche
		if (go_player.transform.position.x < m_v3LimiteGauche.x) {
			if (Direction.DIRECTION_DROITE == directionCourrante) {
				if (m_v3LimiteGauche.x - go_player.transform.position.x < 0.5f) {
						
					deplacement.x = m_iVitesse * Time.deltaTime;
					vitesseDeplacement = 1;
				}
			}
			else if (Direction.DIRECTION_GAUCHE == directionCourrante)
			{
				// Repositionner la Camera pour l'autre bord
				deplacement.x = - m_iVitesse * Time.deltaTime * 2.0f;
				vitesseDeplacement = -2;
			}
		}

		// 2. Si le joueur se trouve devant le bord droit
		else if (m_v3LimiteDroite.x < go_player.transform.position.x) {
			if (Direction.DIRECTION_GAUCHE == directionCourrante) {
				if (go_player.transform.position.x - m_v3LimiteDroite.x < 0.5f) {
					
					deplacement.x = - m_iVitesse * Time.deltaTime;
					vitesseDeplacement = -1;
				}
			}
			else if (Direction.DIRECTION_DROITE == directionCourrante)
			{
				// Repositionner la Camera pour l'autre bord
				deplacement.x = m_iVitesse * Time.deltaTime * 2.0f;
				vitesseDeplacement = 2;
			}
		}

		// 3. Si le joueur se trouve entre les deux bords
		else
		{
			if (Direction.DIRECTION_DROITE == directionCourrante
				&& go_player.transform.position.x < m_v3LimiteDroite.x + 0.5f)
			{
//				 Repositionner la Camera pour l'autre bord
				deplacement.x = m_iVitesse * Time.deltaTime * 2.0f;
				vitesseDeplacement = 2;
			}

			if (Direction.DIRECTION_GAUCHE == directionCourrante
				&& m_v3LimiteGauche.x - 0.5f < go_player.transform.position.x)
			{
				// Repositionner la Camera pour l'autre bord
				deplacement.x = - m_iVitesse * Time.deltaTime * 2.0f;
				vitesseDeplacement = -2;
			}
		}
		transform.Translate (deplacement);
		mettreAJourPositionCentreCamera ();
		mettreAJourPositionToLookAt (vitesseDeplacement);
	}

	/*** Debug ***/
	void afficheDonnees ()
	{
		Debug.Log ("m_fDistanceCentreBord : " + m_fDistanceCentreBord);
		Debug.Log ("m_fDistanceJoueurEnY : " + m_fDistanceJoueurEnY);
		Debug.Log ("m_fDistanceJoueurEnZ : " + m_fDistanceJoueurEnZ);
		Debug.Log ("m_iVitesse : " + m_iVitesse);
		Debug.Log ("m_v3CentreCamera : " + m_v3CentreCamera.x +  "-" + m_v3CentreCamera.y + "-" + m_v3CentreCamera.z);
		Debug.Log ("m_v3LimiteDroite : " + m_v3LimiteDroite.x +  "-" + m_v3LimiteDroite.y + "-" + m_v3LimiteDroite.z);
		Debug.Log ("m_v3LimiteGauche : " + m_v3LimiteGauche.x +  "-" + m_v3LimiteGauche.y + "-" + m_v3LimiteGauche.z);
	}

}



