using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManagerScript : MonoBehaviour {

	public static List<string> deathReasons;

	void Awake(){
		if (GameObject.FindGameObjectsWithTag("Manager").Length > 1){
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(transform.gameObject);
		deathReasons = new List<string>();
	}

	/**
		Called when the player dies to handle all the relevant logic
	 */
	public void Death(string reason){
		// Up the death count
		deathReasons.Add(reason);
		
		// Play the death sound
		// TODO: Actually have a death sound
		
		// Reset the scene
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}

	public void ShowRepeaterTutorial(){
		return; //TODO
	}
}
