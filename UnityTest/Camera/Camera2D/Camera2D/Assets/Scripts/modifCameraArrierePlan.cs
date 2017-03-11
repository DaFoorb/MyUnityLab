using UnityEngine;
using System.Collections;

public class modifCameraArrierePlan : MonoBehaviour {

	public float m_fMoveZ ;
 
	GameObject go_MainCamera ;
	GameObject go_Joueur ;

	bool m_bEstTriggered ;

	Vector3 m_v3DernierePositionCamera ;
	float m_fDernierePositionZoomZ ; // A définir

	Vector3 m_v3Zoom ;

	 // Use this for initialization
	void Start () {
		//this.GetComponent<MeshRenderer> ().enabled = false ;

		m_fDernierePositionZoomZ = 35;

		go_MainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		go_Joueur = GameObject.FindGameObjectWithTag ("Player");

		m_bEstTriggered= false ;
		m_v3DernierePositionCamera = Vector3.zero;

	}
	
	// Update is called once per frame
	void
	Update ()
	{
		zoom () ;
	}

	void
	OnTriggerEnter(Collider collider)
	{
		Debug.Log ("OnTriggerEnter") ;
		if (collider.tag == "Player")
		{
			if (!m_bEstTriggered)
				m_bEstTriggered= true ;
		}
	}

	void
	zoom ()
	{
		Debug.Log ("fonction zoom");
		if(m_bEstTriggered
			&& -m_fDernierePositionZoomZ <= go_MainCamera.transform.position.z)
		{
//			Debug.Log ("fonction zoom : true");
			/* On garde en mémoire la position du joueur avant sa modification */
			if(Vector3.zero == m_v3DernierePositionCamera)
				m_v3DernierePositionCamera = go_MainCamera.transform.position;

			/* On modifie la camera pour voir l'objet à l'arrière plan*/
			Direction directionCourrante = go_Joueur.GetComponent<Player> ().getDirectionCourrante ();

//			Debug.Log ("fonction zoom : " + directionCourrante);

			if (Direction.DIRECTION_DROITE == directionCourrante)
			{
//				Debug.Log ("fonction zoom : Dezoom");
				Vector3 tmp= new Vector3 (0, 0, -m_fMoveZ * Time.deltaTime);
				go_MainCamera.GetComponent<CameraBehavior2> ().mettreAJourPositionCamera (tmp);
			}
			
			else if (Direction.DIRECTION_GAUCHE == directionCourrante)
			{
//				Debug.Log ("fonction zoom : Zoom");
				Vector3 tmp= new Vector3 (0, 0, -m_fMoveZ * Time.deltaTime);
				go_MainCamera.GetComponent<CameraBehavior2> ().mettreAJourPositionCamera (tmp);
			}

		}
	}

}



