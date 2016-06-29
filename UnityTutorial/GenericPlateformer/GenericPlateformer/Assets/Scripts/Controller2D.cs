using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BoxCollider2D))]

public class Controller2D : MonoBehaviour {

	public LayerMask collisionMask;

	const float skinWidth= .015f ; // épaisseur du bounds
	public int horizontalRayCount = 4 ;
	public int verticalRayCount = 4 ;

	// Used for calculating space between each vertical ray and each horizontal ray
	float horizontalRaySpacing ;
	float verticalRaySpacing ;

	BoxCollider2D collider ;
	RaycastOrigins raycastOrigins;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start() {

		collider= GetComponent<BoxCollider2D>() ;
		CalculateRaySpacing();
	}


	/// <summary>
	/// 
	/// </summary>
	/// <param name="velocity">Current velocity</param>
	public void Move(Vector3 velocity) {

		UpdateRaycastOrigins();

		if (velocity.x != 0){
			HorizontalCollision(ref velocity);
		}

		if (velocity.y != 0) {
			VerticalCollision(ref velocity);
		}

		transform.Translate(velocity);
	}

	/// <summary>
	/// Horizontals the collision.
	/// </summary>
	/// <param name="velocity">Velocity.</param>
	void HorizontalCollision(ref Vector3 velocity) {
		// On récupère la direction donnée sur l'axe x, défini par le signe de la vitesse
		float directionX = Mathf.Sign (velocity.x);
		// On calcule la longueur du rayon
		float rayLenght = Mathf.Abs(velocity.x) + skinWidth;
		// Pour chaque ligne...
		for (int i= 0;
		     i < horizontalRayCount;
		     i++) {
			// ... On définit l'origine du rayon
			Vector2 rayOrigin = (directionX ==  -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
			// On met à jour la valeur du rayon d'origine
			rayOrigin += Vector2.up * (horizontalRaySpacing * i) ;
			// On crée un touché de lancée de rayon (lol)

			RaycastHit2D hit = Physics2D.Raycast(rayOrigin,
			                                     Vector2.right * directionX,
			                                     rayLenght,
			                                     collisionMask);
			Debug.DrawRay (rayOrigin,
			               Vector2.right * directionX * rayLenght,
			               Color.red);

			if (hit) {
				velocity.x = (hit.distance - skinWidth) * directionX;
				rayLenght = hit.distance;
			}
		}
	}


	/// <summary>
	/// Verticals the collision.
	/// </summary>
	/// <param name="velocity">Velocity.</param>
	void VerticalCollision(ref Vector3 velocity) {
		float directionY = Mathf.Sign (velocity.y);
		float rayLenght = Mathf.Abs(velocity.y) + skinWidth;
		for (int i= 0;
		     i < verticalRayCount;
		     i++) {
			Vector2 rayOrigin = (directionY ==  -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x) ;
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin,
			                                     Vector2.up * directionY,
			                                     rayLenght,
			                                     collisionMask);
			Debug.DrawRay (rayOrigin,
			               Vector2.up * directionY * rayLenght,
			               Color.red);

			if (hit) {
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLenght = hit.distance;
			}
		}
	}


	/// Get the bounds about collider
	void UpdateRaycastOrigins() {
		
		Bounds bounds = collider.bounds ; // bounds : Délimitation du collider
		bounds.Expand (skinWidth * -2); // Bounds.Expand : Etend la taille du bounds (Bounds.size) par la valeur indiquée

		// On associe les différents coins leurs valeurs respectives
		// Positions basées sur le milieu du GameObject
		// min : centre - extension
		// max : centre + extension
		raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
	}

	/// <summary>
	/// Calculates the ray spacing.
	/// </summary>
	void CalculateRaySpacing() {
		Bounds bounds = collider.bounds ;
		bounds.Expand (skinWidth * -2);

		//Limite le nombre de lignes horizontales à dessiner (entre 2 et la valeur maximum pur un int)
		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue); 
		// Idem pour les lignes verticales
		verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue); 

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);

	}

	// Struct to store corners position of the collider of the GameObject 
	struct RaycastOrigins {
		public Vector2 topLeft, topRight ;
		public Vector2 bottomLeft, bottomRight ;
	}
}

