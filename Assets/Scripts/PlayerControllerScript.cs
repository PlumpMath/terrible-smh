﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {

    public LayerMask layerMask;
    public float jumpHeight;
    public float speed;
    public bool debug;

	bool isJumping;
	float normalizedHorizontalSpeed;

	bool isRecording;
	List<Vector3> movements;

    Rigidbody2D rb2d;
	Vector2 topLeft;
	Vector2 topRight;
	Vector2 botLeft;
	Vector2 botRight;

    void Awake()
	{
		Collider2D c2d = gameObject.GetComponent<Collider2D>();
		rb2d = gameObject.GetComponent<Rigidbody2D>();

		topLeft = new Vector2(-c2d.bounds.extents.x, c2d.bounds.extents.y);
		topRight = new Vector2(c2d.bounds.extents.x, c2d.bounds.extents.y);
		botLeft = new Vector2(-c2d.bounds.extents.x, -c2d.bounds.extents.y);
		botRight = new Vector2(c2d.bounds.extents.x, -c2d.bounds.extents.y);
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.RightArrow)){
			normalizedHorizontalSpeed = 1;
		}
		else if(Input.GetKey(KeyCode.LeftArrow)){
			normalizedHorizontalSpeed = -1;
		}
		else{
			normalizedHorizontalSpeed = 0;
		}

		// Deal with jumping logic
		PerformJumps();

		// Get the distance we want to move on the X
		float idealDistance = speed * Time.deltaTime * normalizedHorizontalSpeed;

		// Check how far we can move on the X
		float actualDistance = getMaxMovementOnX(idealDistance > 0, idealDistance);

		// Compensate for direction
		actualDistance = normalizedHorizontalSpeed > 0 ? actualDistance : -actualDistance;
		
		// Move
		transform.position += new Vector3(actualDistance, 0f);

		// If we're recording, record our position
		if (isRecording)
			movements.Add(transform.position);
	}

	float getMaxMovementOnX(bool goingRight, float maxDistance){
		// Default to however long
		float maxMovement = Mathf.Abs(maxDistance);

		// Get the origins for the rays
		Vector2 topOrigin = rb2d.position + (goingRight ? topRight : topLeft);
		Vector2 botOrigin = rb2d.position + (goingRight ? botRight : botLeft);

		// Get the directions for the rays
		Vector2 rayDir = goingRight ? Vector2.right : Vector2.left;

		// Cast them
		RaycastHit2D topRay = Physics2D.Raycast(topOrigin, rayDir, maxMovement, layerMask);
		RaycastHit2D botRay = Physics2D.Raycast(botOrigin, rayDir, maxMovement, layerMask);

		DebugRay(topOrigin, rayDir);
		DebugRay(botOrigin, rayDir);
		
		// Check if the distance is less than the already established max movement
		if (topRay && Mathf.Abs(topRay.distance) < maxMovement)
			// If so, set the distance to be the max movement
			maxMovement = topRay.distance;
		// ""
		if (botRay && Mathf.Abs(botRay.distance) < maxMovement)
			// ""
			maxMovement = botRay.distance;


		// Return the max movement
		return maxMovement;
		
	}

	void DebugRay(Vector2 origin, Vector2 dir){
		if (debug){
			Debug.DrawRay(origin, dir);
		}
	}

	bool isGrounded(){
		// Get the origins for the rays
		Vector2 leftOrigin = rb2d.position + botLeft;
		Vector2 rightOrigin = rb2d.position + botRight;

		// Get the directions for the rays
		Vector2 rayDir = Vector2.down;

		// Cast them
		RaycastHit2D leftRay = Physics2D.Raycast(leftOrigin, rayDir, 0.1f, layerMask);
		RaycastHit2D rightRay = Physics2D.Raycast(rightOrigin, rayDir, 0.1f, layerMask);

		DebugRay(rightOrigin, rayDir);
		DebugRay(leftOrigin, rayDir);
		
		// If either of them hit return true, otherwise we're in the air
		return leftRay || rightRay;
	}

	void PerformJumps()
	{
		// Reset the velocity if we're not jumping or have just landed.
		if (!isJumping){
			// rb2d.velocity = Vector2.zero;
		}else if (isGrounded()){
			isJumping = false;
			// rb2d.velocity = Vector2.zero;
		}

		// Check if we want to jump & can
		if (Input.GetKey(KeyCode.UpArrow) && !isJumping && canJump()){
			// Add a force and record that we're jumping
			rb2d.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
			isJumping = true;
			return;
		}
	}

	bool canJump(){
		return isGrounded();
	}

	internal void startRecording(ref List<Vector3> movements)
    {
        isRecording = true;
		movements.Clear();
		this.movements = movements;
    }

    internal void stopRecording()
    {
        isRecording = false;
    }

}
