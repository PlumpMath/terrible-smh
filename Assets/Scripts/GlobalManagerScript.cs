using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManagerScript : MonoBehaviour {

	public static List<string> deathReasons;

	public GameObject repeaterTutorial;
    public GameObject recordingOverlayPrefab;

	GameObject recordingOverlay;
	AudioSource beepSource;

    void Awake(){
		if (GameObject.FindGameObjectsWithTag("Manager").Length > 1){
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(transform.gameObject);
		deathReasons = new List<string>();
		beepSource = gameObject.GetComponent<AudioSource>();
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
		Instantiate(repeaterTutorial, Vector3.zero, Quaternion.identity);
	}

	public void showRecordingOverlay()
	{
		recordingOverlay = Instantiate(recordingOverlayPrefab, Vector3.zero, Quaternion.identity);
	}

	public void hideRecordingOverlay()
	{
		if (recordingOverlay)
			Destroy(recordingOverlay);
	}

	public void beep()
	{
		beepSource.Play();
	}

}
