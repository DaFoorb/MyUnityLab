using UnityEngine;
using System.Collections;

// [RequireComponent(typeof(Controller2D))]

public class Player : MonoBehaviour {

	float moveSpeed = 6 ;
	float gravity = -20;
	Vector3 velocity;

	// Controller2D controller ;
	// Rigidbody rigidbody ;
	
	void Start() {

		// Having reference to the controller
		// controller= GetComponent<Controller2D>() ;
		// rigidbody = this.GetComponent<Rigidbody>();
	}

	void InputPC() {
		if (Input.GetKey("left"))
		{
			GetComponent<Rigidbody>().AddForce (-Vector3.forward);
		}
		else if (Input.GetKey("right"))
		{
			GetComponent<Rigidbody>().AddForce (Vector3.forward);
		}
	}

	void Update () {
		GetComponent<Rigidbody>().AddForce(-Vector3.right);

		// On récupère la position courrante
		// GetAxisRaw récupère la position selon l'orientation demandé
		// Les touches flèches droite et gauche sont par défaut mappé sur la fonction
		/*
		Vector2 input = new Vector2(Input.GetAxisRaw ("Horizontal"),
		                            Input.GetAxisRaw ("Vertical")) ;

		// vitesse horizontal vaut sa position multipliée par la vitesse de déplacement
		velocity.x = input.x * moveSpeed;
		// vitesse horizontale vaut la gravité miltipliée par le temps d'une frame
		velocity.y += gravity * Time.deltaTime;

		controller.Move(velocity * Time.deltaTime);*/
	}
}
