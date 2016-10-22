using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	/*** Publique ***/
	public int posX ;
	public int posY ;
	public int posZ ;
	public int m_iVitesse;


	/*** Privée ***/
	GameObject go_level ;
	GameObject go_player ;
	GameObject go_limite ;

	int m_iCameraMode ;

	bool m_bEstRepositionne ;
	bool m_bEstImmobile ;

	float m_fLimiteDroite ;
	float m_fLimiteGauche ;
	float m_fDistance ;
	float m_fRatio ;
	float m_fScreenWidth= Screen.width ;
	float m_fScreenHeight= Screen.height ;
	float m_fPositionBordDroit ;

	const float _F_DISTANCEZCAMERAJOUEUR= -20.0f ;
	float _F_DISTANCELIMITE ;

	float _F_RATIOXPOSITIONBORD ;

	Vector3 m_v3DernierePositionPlayer ;

	// Use this for initialization
	void
	Start ()
	{
		go_player= GameObject.FindGameObjectWithTag ("Player") ;
		go_limite= GameObject.FindGameObjectWithTag ("Limite") ;
		go_level= GameObject.FindGameObjectWithTag ("Niveau") ;

		_F_RATIOXPOSITIONBORD= Screen.width / 17 * 0.5f ;
		_F_DISTANCELIMITE= (Screen.width / 17) * 0.02f ;
		Debug.Log ("_F_DISTANCELIMITE : " + _F_DISTANCELIMITE);

		m_bEstRepositionne= false ;
		m_bEstImmobile= false ;
		m_iCameraMode = 3;

		m_v3DernierePositionPlayer= Vector3.zero;

		m_fDistance= go_level.transform.localScale.x + 30.0f ;
		m_fRatio= m_fDistance/go_level.transform.localScale.x ;
	}
	
	// Update is called once per frame
	void 
	Update ()
	{
		Debug.Log ("Resolution : " + Screen.width + " - " + Screen.height);
		positionCamera ();
		cameraMode ();
		changeCameraMode ();
	}


	void
	positionCamera ()
	{
		if (3 == m_iCameraMode && !m_bEstRepositionne) {
			this.transform.position = 
				new Vector3 (
				this.transform.position.x,
				this.transform.position.y,
				this.transform.position.z + _F_DISTANCEZCAMERAJOUEUR);
			
			this.transform.LookAt (go_player.transform);
			m_bEstRepositionne = true;
		}
		else
		if(3 != m_iCameraMode)
		{
			m_bEstRepositionne = false;
		}
	}

	void 
	changeCameraMode()
	{
		if (Input.GetKey("1")) 
			m_iCameraMode= 1 ;

		if (Input.GetKey("2")) 
			m_iCameraMode= 2 ;

		if (Input.GetKey("3")) 
			m_iCameraMode= 3 ;
	}

	/// <summary>
	/// Manage the camera mode
	/// </summary>
	void 
	cameraMode () 
	{
		switch (m_iCameraMode)
		{
			case 1:
				m_bEstRepositionne = false;
					// Toujours derrière l'objet
				transform.position = new Vector3 (
					go_player.transform.position.x - posX,
					go_player.transform.position.y + posY,
					go_player.transform.position.z - posZ
				) ;
					
				this.transform.LookAt (go_player.transform.position) ;

				break ;

			case 2:
				m_bEstRepositionne = false ;
				// Tourne autour de l'objet en fonction de la taille du niveau
				transform.position = new Vector3 (
					(go_player.transform.position.x - posX) * m_fRatio,
					go_player.transform.position.y + posY,
					go_player.transform.position.z - posZ
				) ;

				break ;

			case 3:
				speedup_push_zone () ;
				break ;
		}
	}


	/// <summary>
	/// une zone est définie par un bord. Le côté de droite fait bouger la camera lorsque le personnage la touche.
	/// </summary>
	void 
	speedup_push_zone ()
	{
		m_v3DernierePositionPlayer = go_player.transform.position ;

		// Il faut définir un bord par rapport à la position de la camera à chaque frame
		// /!\ Voir le calcul pour _F_DISTANCELIMITE
		m_fLimiteDroite = this.transform.position.x + _F_DISTANCELIMITE	;
		m_fLimiteGauche = this.transform.position.x - _F_DISTANCELIMITE*5;

		// Si le personnage dépasse cette limite, alors le personnage pousse la camera
		if(go_player.transform.position.x > m_fLimiteDroite)
		{
			this.transform.position=
				new Vector3 (this.transform.position.x + 10*Time.deltaTime, 
					this.transform.position.y,
					this.transform.position.z);
		}

		if(go_player.transform.position.x < m_fLimiteGauche)
		{
			this.transform.position=
				new Vector3 (this.transform.position.x - 10*Time.deltaTime, 
					this.transform.position.y,
					this.transform.position.z);
		} 
	}


	public
	float
	getBordDroit()
	{
		return m_fPositionBordDroit;
	}


}


