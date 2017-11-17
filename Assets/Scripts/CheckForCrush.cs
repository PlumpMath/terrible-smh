using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.wikihow.com/Deal-With-a-Crush
public class CheckForCrush : MonoBehaviour {
	
	Transform manager;

	void Start()
	{
		manager = GameObject.FindGameObjectWithTag("Manager").transform;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		manager.gameObject.GetComponent<GlobalManagerScript>().Death("Crushed to death");
	}
}
