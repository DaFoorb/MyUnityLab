using UnityEngine;
using System.Collections.Generic; // --> Pour avoir les List

public class Player : MonoBehaviour {

	public

	int i_id ;
	string st_name ;
	int i_Life ;
	int i_damageGiven ; 
	List<GenericPower> tab_Power ;

	float f_castPower ;
	bool b_isCasted ;

	public GameObject boule ;



	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		i_id= 1 ;
		st_name= "Enemy 1" ;
		i_Life = 10 ;
		i_damageGiven = 5 ;
		f_castPower = 5.0f ;
		b_isCasted = false;

		tab_Power= new List<GenericPower>(0);
		tab_Power.Clear ();
	}




	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		if (b_isCasted) {
			f_castPower -= Time.deltaTime;

			if (0 >= f_castPower ) {
				b_isCasted= false;
			}
		} else {
			f_castPower= 5.0f;
		}
		controlPower ();
	}



	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collisionInfo">Collision info.</param>
	void OnCollisionEnter (Collision collisionInfo)
	{
		if ("PowerSpawn(Clone)" == collisionInfo.collider.name)
		{
			GameObject powerSpawn= GameObject.FindGameObjectWithTag("PowerSpawn") ;

			// On récupère l'id du pouvoir de l'object PowerSpawn
			int idPower= powerSpawn.GetComponent<PowerSpawn> ().getIdPower ();
			//Color powerSpawnColor = powerSpawn.GetComponent<Material> ().color;

			// On récupère le tableau des pouvoirs
			GenericPower[] tmp= PowerList.GetPowerList() ;

			// On parcourt les pouvoirs
			foreach(GenericPower power in tmp) 
			{
				// Si le pouvoir récupéré correspond au courrant du tableau
				if (idPower == power.getIdPower ()) 
				{
					// Alors on l'ajoute dans la liste des pouvoirs débloqué par le joueur.
					tab_Power.Add (power);
				}
			}
		}
	}



	/// <summary>
	/// Controls the power.
	/// </summary>
	void controlPower ()
	{
		
		if (Input.GetKey("p") 
			&& tab_Power.Count != 0
			&& !b_isCasted)
		{
			b_isCasted = true;
			GameObject go = Instantiate(boule) ;
		}
	}



	/// <summary>
	/// Gets the attack.
	/// </summary>
	/// <returns>The attack.</returns>
	public int getDamageGiven()
	{
		return i_damageGiven;
	}

}
