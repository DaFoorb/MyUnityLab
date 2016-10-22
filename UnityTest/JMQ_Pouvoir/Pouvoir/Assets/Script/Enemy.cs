using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject prefab_PowerSpawn ;

	int i_id ;
	string st_name ;
	int i_idPower ;
	int i_Life ;

	/// <summary>
	/// Start the enemy instance.
	/// </summary>
	void Start () {
		i_id = 1 ;
		st_name = "Enemy 1" ;
		i_idPower = 1 ;	
		i_Life = 10 ;
	}
	
	// Update is called once per frame
	void Update ()
	{}


	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collisionInfo">Collision info</param>
	void OnCollisionEnter (Collision collisionInfo)
	{
		if ("Player" == collisionInfo.collider.tag
			|| "Power" == collisionInfo.collider.tag) 
		{
			if ("Player" == collisionInfo.collider.tag) {
				i_Life -= GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().getDamageGiven ();
			}

			if ("Power" == collisionInfo.collider.tag) {
				i_Life -= GameObject.FindGameObjectWithTag ("Power").GetComponent<ProjectilBehavior> ().getDegats ();
				GameObject.FindGameObjectWithTag ("Power").GetComponent<ProjectilBehavior> ().setIsDead (true) ;
			}
				
			// Si  l'ennemi n'a plus de vie
			if (0 >= i_Life) 
			{
				// On instancie l'objet pouvoir a recup		if (null != prefab_PowerSpawn)
				if (null != prefab_PowerSpawn) {
					CreatePowerSpawn ();
				}
				// On détruit l'ennemi après avoir crééer le power spawn, sinon erreur
				destroyGameObject() ;
			}

		}
	}



	/// <summary>
	/// Creates the power spawn after death
	/// </summary>
	void CreatePowerSpawn ()
	{
		Instantiate(prefab_PowerSpawn, 
			new Vector3 (transform.position.x,
						 5,
						 transform.position.z), 
			new Quaternion(transform.rotation.x,
						   transform.rotation.y,
						   transform.rotation.z,
						   transform.rotation.w)
		) ;
		prefab_PowerSpawn.GetComponent<PowerSpawn>().setIdPower (i_idPower);
	}



	/// <summary>
	/// Destroies the game object.
	/// </summary>
	public void destroyGameObject() 
	{
		Destroy (gameObject);
	}

}
