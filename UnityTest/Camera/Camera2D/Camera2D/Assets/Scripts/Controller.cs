using UnityEngine;
using System.Collections;


public class Controller : MonoBehaviour {

	GameObject go_player;
	GameObject go_sol;

	/*** Variables membres ***/
	public int i_vitesse ;

		/*** Saut ***/
	public float m_fHauteurSaut;
	public float m_fTempsSautMaxAAtteindre;

	float m_fGravity;
	float m_fVitesseSaut;
	Vector3 m_v3mouvement ;

	bool m_bEstSaut;

		/*** Fonctions ***/
	void Start()
	{
		go_player= GameObject.FindGameObjectWithTag ("Player");
		go_sol= GameObject.FindGameObjectWithTag ("Niveau");

		m_fGravity= - (2 * m_fHauteurSaut) / Mathf.Pow (m_fTempsSautMaxAAtteindre, 2);
		m_fVitesseSaut = Mathf.Abs(m_fGravity) * m_fTempsSautMaxAAtteindre ;

		Debug.Log ("m_fGravity : " + m_fGravity + " - m_fVitesseSaut : " + m_fVitesseSaut);

		m_v3mouvement = Vector3.zero;

		m_bEstSaut = false;
	}

	void
	Update()
	{
		mouvementGeneral();
	}

	/// Handle movement of the charactere (square).
	void
	mouvementGeneral ()
	{
		/*** Déplacement Sol ***/
		float fDeplacementH= 0.0f;

		if (Input.GetKey ("right"))
			fDeplacementH= i_vitesse;
		
		else if (Input.GetKey ("left"))
			fDeplacementH= - i_vitesse;

		mettreAJourDirection (fDeplacementH);

		/*** Saut ***/
		// https://www.youtube.com/watch?v=PlT44xr0iW0
		// [Unity] Creating a 2D Platformer (E03. jump physics)

		if(!m_bEstSaut)
			m_v3mouvement.y = 0;

		if (Input.GetKeyDown ("up") && !m_bEstSaut) {
			m_bEstSaut = true;
			m_v3mouvement.y = m_fVitesseSaut ;
		}

		if (m_bEstSaut)
			m_v3mouvement.y += m_fGravity * Time.deltaTime;
			
		/*** Mise à jour ***/
		m_v3mouvement.x = fDeplacementH;

		transform.Translate (m_v3mouvement * Time.deltaTime);
	}

	void
	mettreAJourDirection (float p_fSign)
	{	
		if (0 < p_fSign)
			go_player.GetComponent<Player> ().setDirectionCourrante (Direction.DIRECTION_DROITE);
		
		else if (0 > p_fSign)
			go_player.GetComponent<Player> ().setDirectionCourrante (Direction.DIRECTION_GAUCHE);
		
		else if (0 == p_fSign)
			go_player.GetComponent<Player> ().setDirectionCourrante (Direction.DIRECTION_INDEFINIE);
	}

	public
	int 
	getVitesse()
	{
		return i_vitesse;
	}

	void
	OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "Niveau")
		{
			m_bEstSaut = false;
			m_v3mouvement.y = 0;	
		}
	}
}

