using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeaterScript : MonoBehaviour {
	
	public GameObject actorPrefab;

	List<Vector3> movements = new List<Vector3>();

	bool isRecording;

	PlayerControllerScript pcs;

	GameObject actor;

	GlobalManagerScript manager;

	bool playKeyPressed;

	void Start()
	{
		manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GlobalManagerScript>();
	}

	void Awake()
	{
		pcs = gameObject.GetComponent<PlayerControllerScript>();
	}

	void Update () {
		// When T is pressed, start recording the player's movements
		if (Input.GetKey(KeyCode.T) && !isRecording){
			Debug.Log("Player started recording");
			manager.showRecordingOverlay();
			manager.beep();
			pcs.startRecording(ref movements);
			isRecording = true;
		}
		// When R is pressed, stop.
		if (Input.GetKey(KeyCode.R) && isRecording){
			Debug.Log("Player stopped recording");			
			pcs.stopRecording();
			manager.hideRecordingOverlay();
			manager.beep();
			isRecording = false;
		}
		// When Q is pressed, make a new object that re-enacts those movements
		if (Input.GetKey(KeyCode.Q) && !isRecording && !playKeyPressed){
			if (actor)
				Destroy (actor);

			Debug.Log("Started playback");
			actor = Instantiate(actorPrefab, movements[0], Quaternion.identity);
			manager.beep();			

			actor.GetComponent<RepeaterActor>().movements = new List<Vector3>(movements);
		}

		playKeyPressed = Input.GetKey(KeyCode.Q);
	}
}
