using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputScreen : MonoBehaviour {

	public LayerMask touchInputMask ;

	private List<GameObject> touchList= new List<GameObject>() ; // Permet d'avoir la liste des objets à utiliser
	private GameObject[] touchesOld ; // Permet de sauvegarder les objets utilisés précédemment
	private RaycastHit hit ;


	void Update () {


// Nécessaire afin de pouvoir tetser sur l'IDE
#if UNITY_EDITOR

		//Si on récupère n'importe quelle action de la souris...
		if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) {
			// ... alors on peut gérer l'action de l'utilisateur

			// A chaque frame on gère les objets utilisés et à sauvegarder
			touchesOld= new GameObject[touchList.Count] ;
			touchList.CopyTo(touchesOld) ;
			touchList.Clear () ;

			// On définit le rayon pour détecter les objets au clic souris
			Camera camera= this.GetComponent<Camera>() ;
			Ray ray= camera.ScreenPointToRay(Input.mousePosition) ;

			// Si le raycast rencontre un objet avec un mayermask (?)...
			if (Physics.Raycast(ray, out hit, touchInputMask)) {
				//... alors on gère l'object percuté.

				// On récupère l'objet touché par le lancée de rayon (enregistré dans le RaycastHit)
				GameObject recipient= hit.transform.gameObject;

				// On ajoute l'objet dans la liste des objets touchés
				touchList.Add (recipient) ;

				// On gère le type d'action entrepris par le joueur
				if (Input.GetMouseButton(0)) {
					recipient.SendMessage ("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver) ;
				}
				
				if (Input.GetMouseButtonUp(0)) {
					recipient.SendMessage ("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver) ;
				}
				
				if (Input.GetMouseButtonDown(0)) {
					recipient.SendMessage ("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver) ;
				}
			}

			// Pour tout objet stocké dans la liste des anciens objets
			foreach(GameObject g in touchesOld) {
				// Si l'object n'existe pas dans la liste des objets touchés...
				if (!touchList.Contains(g)) {
					//... On gère comme si le joueur avait déselectionné l'objet.
					g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver) ; ;
				}
			}
		}

#endif
		//Si on récupère n'importe quelle action du joueur sur l'écran...
		if (Input.touchCount > 0 ) {
			// ... alors on peut gérer l'action de l'utilisateur
			
			// A chaque frame on gère les objets utilisés et à sauvegarder
			touchesOld= new GameObject[touchList.Count] ;
			touchList.CopyTo(touchesOld) ;
			touchList.Clear () ;

			// Pour toute action du joueur sur l'écran
			foreach (Touch touch in Input.touches) {
				// On gère l'action

				// On définit le rayon pour détecter les objets au clic souris
				Camera camera= this.GetComponent<Camera>() ;
				Ray ray= camera.ScreenPointToRay(touch.position) ;

				//Si on récupère n'importe quelle action de la souris...
				if (Physics.Raycast(ray, out hit, touchInputMask)) {
					//... alors on gère l'object percuté.
					
					// On récupère l'objet touché par le lancée de rayon (enregistré dans le RaycastHit)
					GameObject recipient= hit.transform.gameObject;

					// On ajoute l'objet dans la liste des objets touchés
					touchList.Add (recipient) ;

					// On gère le type d'action entrepris par le joueur
					if (touch.phase == TouchPhase.Began) {
						recipient.SendMessage ("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver) ;
					}

					if (touch.phase == TouchPhase.Ended) {
						recipient.SendMessage ("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver) ;
					}

					if (touch.phase == TouchPhase.Stationary) {
						recipient.SendMessage ("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver) ;
					}

					if (touch.phase == TouchPhase.Canceled) {
						recipient.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver) ;
					}
				}
			}

			// Pour tout objet stocké dans la liste des anciens objets
			foreach(GameObject g in touchesOld) {
				// Si l'object n'existe pas dans la liste des objets touchés...
				if (!touchList.Contains(g)) {
					//... On gère comme si le joueur avait déselectionné l'objet.
					g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver) ; ;
				}
			}
		}
	}
}
