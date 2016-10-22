using UnityEngine;
using System.Collections;

public class PowerSpawn : MonoBehaviour {

	static int i_IdPower ;

	// Use this for initialization
	void Start ()
	{}



	// Update is called once per frame
	void Update ()
	{}



	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collisionInfo">Collision info.</param>
	void OnCollisionEnter(Collision collisionInfo)
	{
		if ("Joueur" == collisionInfo.collider.name)
		{
			Destroy (gameObject);
		}

	}



	/// <summary>
	/// Gets the power identifier.
	/// </summary>
	/// <returns>The power identifier.</returns>
	public int getIdPower () 
	{
		return i_IdPower;
	}


	/// <summary>
	/// Sets the identifier power.
	/// </summary>
	/// <param name="p_i_idPower">P i identifier power.</param>
	public void setIdPower(int p_i_idPower) 
	{
		i_IdPower = p_i_idPower ;
	}
}
