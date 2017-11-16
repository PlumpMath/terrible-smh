using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoxScript : MonoBehaviour {

	public Transform manager;

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("hello");
		// Check if it's the player
		if (other.gameObject.tag == "Player"){
			// If so, fire the death event on the global manager
			manager.gameObject.GetComponent<GlobalManagerScript>().Death("Fell out of the world");
		}
	}
}
