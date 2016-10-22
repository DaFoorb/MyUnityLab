using UnityEngine;
using System.Collections;




public class Controller : MonoBehaviour {

	public int i_vitesse ;
	public int i_degat ;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{}



	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update() {
		Move();
	}




	/// <summary>
	/// Handle movement of the charactere (square).
	/// </summary>
	void Move () {
		
		if (Input.GetKey("right"))
		{			
			this.transform.position= new Vector3 (this.transform.position.x + i_vitesse*Time.deltaTime, 
												  this.transform.position.y, 
												  -10);
		}
		else if (Input.GetKey("left"))
		{
			transform.position= new Vector3 (transform.position.x - i_vitesse*Time.deltaTime, 
										     transform.position.y, 
										     -10);
		}
	}
}

