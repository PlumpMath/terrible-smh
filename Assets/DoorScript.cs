using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	public Vector3 openPosition;

	public Vector3 closedPosition;

	public float time;

	Vector3 targetPosition;

	Vector3 velocity;

	void Update()
	{
		// TODO: Ideally, the collider would move instantly and only the appearance would be a smooth transition
		if (targetPosition != Vector3.zero) {
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, time);

			if (targetPosition == transform.position)
				targetPosition = Vector3.zero;
		}
	}

	public void open()
	{
		targetPosition = openPosition;
	}

	public void close()
	{
		targetPosition = closedPosition;
	}
}
