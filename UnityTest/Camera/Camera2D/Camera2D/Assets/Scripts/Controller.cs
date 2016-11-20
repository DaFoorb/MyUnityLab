using UnityEngine;
using System.Collections;


public class Controller : MonoBehaviour {

	public int i_vitesse ;

	GameObject go_player;

	void Start()
	{
		go_player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update() {
		Move();
	}

	/// Handle movement of the charactere (square).
	void Move ()
	{
		if (Input.GetKey ("right"))
		{
			this.transform.position = new Vector3 (this.transform.position.x + i_vitesse * Time.deltaTime, 
				this.transform.position.y, 
				this.transform.position.z);
			go_player.GetComponent<Player> ().setDirectionCourrante (Direction.DIRECTION_DROITE);
		} else if (Input.GetKey ("left"))
		{
			this.transform.position = new Vector3 (this.transform.position.x - i_vitesse * Time.deltaTime, 
				this.transform.position.y, 
				this.transform.position.z);
			go_player.GetComponent<Player> ().setDirectionCourrante (Direction.DIRECTION_GAUCHE);
		} else
		{
			go_player.GetComponent<Player> ().setDirectionCourrante (Direction.DIRECTION_INDEFINIE);
		}
			
	}


	public
	int 
	getVitesse()
	{
		return i_vitesse;
	}

}

