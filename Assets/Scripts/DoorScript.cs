using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	public Vector3 openPosition;

	public Vector3 closedPosition;

	public float openTime;

	public float closeTime;

	Vector3 targetPosition;

	bool isClosing;

	Vector3 velocity;

	void Update()
	{
		// TODO: Ideally, the collider would move instantly and only the appearance would be a smooth transition
		if (targetPosition != Vector3.zero) {
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, isClosing ? closeTime : openTime);

			if (targetPosition == transform.position)
				targetPosition = Vector3.zero;
		}
	}

	public void open()
	{
		targetPosition = openPosition;
		isClosing = false;
	}

	public void close()
	{
		targetPosition = closedPosition;
		isClosing = true;
	}
}
