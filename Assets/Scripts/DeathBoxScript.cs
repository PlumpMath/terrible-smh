using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoxScript : MonoBehaviour {

	Transform manager;

	void Start()
	{
		manager = GameObject.FindGameObjectWithTag("Manager").transform;
	}

	void OnTriggerEnter2D(Collider2D other){
		// Check if it's the player
		if (other.gameObject.tag == "Player"){
			// If so, fire the death event on the global manager
			manager.gameObject.GetComponent<GlobalManagerScript>().Death("Fell out of the world");
		}
	}
}
