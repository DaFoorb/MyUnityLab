using UnityEngine;
using System.Collections;

public class PowerList : MonoBehaviour {

	// Singleton
	private static GenericPower[] powersList ;

	// Fonctions membres

	/// <summary>
	/// Accesseur
	/// </summary>
	public static GenericPower[] GetPowerList()
	{
		if (null == powersList)
		{
			powersList = new GenericPower[2] ;

			for (int i = 0 ; i < powersList.Length ; i++)
			{
				if (0 != i)
				{
					powersList [i]= new GenericPower (1, "LA BOULE MAGIQUE", 15) ;
				} else 
				{
					powersList [i]= new GenericPower () ;
				}
			}
		}
			
		return powersList ;
	}
}
