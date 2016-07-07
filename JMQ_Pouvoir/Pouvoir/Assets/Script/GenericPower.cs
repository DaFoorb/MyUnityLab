using UnityEngine;
using System.Collections;

public class GenericPower {

	int i_idPower ;
	string st_powerName ;
	int i_powerDamage ;

	/// <summary>
	/// Construteur par défaut
	/// </summary>
	public GenericPower () {
		
		i_idPower= -1 ;
		st_powerName= "Generic Power" ;
		i_powerDamage= 10 ;
	}
	
	/// <summary>
	/// Construteur (identifiant, nomPouvoir, nombreDégatsInfligés)
	/// </summary>
	public GenericPower (int p_iIdPower, 
						 string p_stPowerName, 
						 int p_iPowerDamage) {

		i_idPower= p_iIdPower ;
		st_powerName= p_stPowerName ;
		i_powerDamage= p_iPowerDamage ;
	}



	/// <summary>
	/// Gets the identifier power.
	/// </summary>
	/// <returns>The identifier power.</returns>
	public int getIdPower()
	{
		return i_idPower ;
	}
}
