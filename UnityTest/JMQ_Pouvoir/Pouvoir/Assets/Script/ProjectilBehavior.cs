using UnityEngine;
using System.Collections;

public class ProjectilBehavior : MonoBehaviour {

	GameObject go_player ;
	float f_velocity ;
	float f_lifeTime ;
	bool b_isDead ;

	int i_degat ;

	/// <summary>
		/// Start this instance.
	/// </summary>
	void Start () {
		go_player= GameObject.FindGameObjectWithTag ("Player");
		transform.position= new Vector3 (go_player.transform.position.x,
											go_player.transform.position.y + 2.5f,
											-10) ;

		i_degat= 15 ; 
		f_velocity= 10;
		f_lifeTime= 20.0f ;
		b_isDead = false;

	}



	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		isDead () ;

		f_lifeTime-= Time.deltaTime ;
		if (0 >= f_lifeTime) 
		{
			b_isDead = true;
		}
	
		Move ();
	}



	/// <summary>
	/// Move this instance.
	/// </summary>
	void Move ()
	{
		transform.position= new Vector3 (transform.position.x + Time.deltaTime*f_velocity,
										 transform.position.y,
										 -10) ;
	}



	/// <summary>
	/// Gets the degats.
	/// </summary>
	/// <returns>The degats.</returns>
	public int getDegats ()
	{
		return i_degat;
	}



	/// <summary>
	/// Sets the is dead.
	/// </summary>
	/// <param name="p_b_isDead">If set to <c>true</c> p b is dead.</param>
	public void setIsDead (bool p_b_isDead)
	{
		b_isDead= p_b_isDead;
	}



	/// <summary>
	/// Ises the dead.
	/// </summary>
	void isDead () 
	{
		if (b_isDead)
		{
			Destroy (gameObject);	
		}
	}
}
