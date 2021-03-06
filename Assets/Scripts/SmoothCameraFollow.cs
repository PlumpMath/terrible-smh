﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour {
	
	public bool moveY;

	public bool moveX;

	public Vector3 minPoint;
	
	public Vector3 maxPoint;

	public Transform objectToFollow;

	public float distanceOnZ;

	public float smoothTime;

	Vector3 velocity = Vector3.zero;

	void Update () {
		// Get the location of the object
		Vector3 objectLoc = objectToFollow.position;
		
		Vector3 undampedLoc = transform.position;
		// If we're meant to move on the Y make sure we do
		if (moveY)
			undampedLoc.y = Mathf.Max(Mathf.Min(objectLoc.y, maxPoint.y), minPoint.y);
		// If we're meant to move on the X make sure we do
		if (moveX)
			undampedLoc.x = Mathf.Max(Mathf.Min(objectLoc.x, maxPoint.x), minPoint.x);

		// Set the right Z distance
		undampedLoc.z = distanceOnZ;
		// Move towards the ideal point
		transform.position = Vector3.SmoothDamp(transform.position, undampedLoc, ref velocity, smoothTime);
	}
}
