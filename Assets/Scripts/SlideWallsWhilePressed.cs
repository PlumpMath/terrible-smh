using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideWallsWhilePressed : MonoBehaviour {

	public List<Transform> doorsToControl;


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			foreach (Transform t in doorsToControl) {
				t.GetComponent<DoorScript> ().open ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			foreach (Transform t in doorsToControl) {
				t.GetComponent<DoorScript> ().close ();
			}
		}
	}
}
