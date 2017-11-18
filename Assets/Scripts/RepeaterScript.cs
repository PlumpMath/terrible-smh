using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeaterScript : MonoBehaviour {
	
	public GameObject actorPrefab;

	List<Vector3> movements = new List<Vector3>();

	bool isRecording;

	PlayerControllerScript pcs;

	GameObject actor;

	void Awake()
	{
		pcs = gameObject.GetComponent<PlayerControllerScript>();
	}

	void Update () {
		// When T is pressed, start recording the player's movements
		if (Input.GetKey(KeyCode.T) && !isRecording){
			Debug.Log("Player started recording");
			isRecording = true;
			pcs.startRecording(ref movements);
			// TODO: Sound, Visuals
		}
		// When R is pressed, stop.
		if (Input.GetKey(KeyCode.R) && isRecording){
			Debug.Log("Player stopped recording");			
			pcs.stopRecording();
			isRecording = false;
		}
		// When Q is pressed, make a new object that re-enacts those movements
		if (Input.GetKey(KeyCode.Q) && !isRecording){
			if (actor)
				Destroy (actor);

			Debug.Log("Started playback");
			actor = Instantiate(actorPrefab, movements[0], Quaternion.identity);

			actor.GetComponent<RepeaterActor>().movements = movements;
		}
	}
}
