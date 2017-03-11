using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	Animation m_animation ;

	// Use this for initialization
	void Start ()
	{		
		m_animation = GetComponent<Animation> ();

		foreach (AnimationState state in m_animation)
		{
			state.speed = 0.5f;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		jouerIdle ();	
	}

	void jouerIdle ()
	{

	}
}
