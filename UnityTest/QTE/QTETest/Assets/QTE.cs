using UnityEngine;
using System.Collections;

public class QTE : MonoBehaviour {

	enum en_stateQTE {
		Sleep,
		Start,
		End
	} ;

	enum en_resultQTE {
		NoResult,
		Successuf,
		Failure
	} ;

	public float f_countdown ;

	public Color defaultColor ;
	public Color QTEColor ;
	public Color successColor ;
	public Color failColor ;
	
	private Material mat ;
	private en_stateQTE currentState ;
	private en_resultQTE currentResult ;
	private bool b_isQTEStarted ;
	private bool b_isPlayerPressedBefore ;
	private bool b_isPlayerPressed ;

	//Pour le test
	public float f_timeBeforeQTE ;

	// Use this for initialization
	void Start () {

		mat= this.GetComponent<Renderer>().material ;

		currentState= en_stateQTE.Sleep ;
		currentResult= en_resultQTE.NoResult ;

		b_isQTEStarted= false ;
		b_isPlayerPressed= false ;
		b_isPlayerPressedBefore= false ;
	}


	
	// Update is called once per frame
	void Update () {
		if (b_isQTEStarted) {
			stateQTEHandle () ;
			resultQTEHandle () ;

			f_countdown-= Time.deltaTime ;
		} else {
			f_timeBeforeQTE-= Time.deltaTime ;

			if (f_timeBeforeQTE < 0.0f) {
				b_isQTEStarted= true ;
			}
		}
	}


	// Trigger during OnTouchDown
	void OnTouchDown () {
		if(currentState == en_stateQTE.Start) {
		   	if (Input.touchCount == 1) {
				b_isPlayerPressed= true ;
			} else if (Input.touchCount > 1) {
				b_isPlayerPressedBefore= true ;
				b_isPlayerPressed= false ;
			}

		}
	}



	// Gestion de l'état du QTE
	void stateQTEHandle () {

		// SLEEP
		if (currentState == en_stateQTE.Sleep) {

			if (b_isQTEStarted) {
				currentState= en_stateQTE.Start ;
			}
		}

		// START
		if (currentState == en_stateQTE.Start) {

			mat.color= QTEColor ;
			if (b_isPlayerPressed 
			    && !b_isPlayerPressedBefore) {

				if (f_countdown > 0.0f) {

					currentState= en_stateQTE.End ;
					currentResult= en_resultQTE.Successuf ;
				} 
			} else if (f_countdown <= 0.0f) {
				
				currentState= en_stateQTE.End ;
			}
		}

		// END
		if (currentState == en_stateQTE.End) {

			if (currentResult == en_resultQTE.NoResult) {
				currentResult= en_resultQTE.Failure ;
				b_isQTEStarted= false ;
			}
		}
	}

	

	// Gestion du résultat du QTE
	void resultQTEHandle () {

		if (currentResult == en_resultQTE.Successuf) {
			mat.color= successColor ;
		}

		if (currentResult == en_resultQTE.Failure) {
			mat.color= failColor ;	
		}
	}
}
