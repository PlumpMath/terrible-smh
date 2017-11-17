using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideWallsWhilePressed : MonoBehaviour {

	public List<Transform> doorsToControl;

	public LayerMask playerLayer;

	ContactFilter2D cf2d;

	Collider2D collider;

	Collider2D[] contacts;

	bool isOpen;

	void Awake()
	{
		collider = gameObject.GetComponent<Collider2D> ();
		cf2d = new ContactFilter2D ();
		cf2d.SetLayerMask (playerLayer);
	}

	void Update()
	{
		
		if (collider.OverlapCollider (cf2d, ref contacts) > 0) {
			foreach (Transform t in doorsToControl) {
				t.GetComponent<DoorScript> ().open ();
			}
			isOpen = true;
		} else if (isOpen) {
			foreach (Transform t in doorsToControl) {
				t.GetComponent<DoorScript> ().close ();
			}
			isOpen = false;
		}
	}
}
