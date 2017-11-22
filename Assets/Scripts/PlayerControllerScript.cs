using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {

    public LayerMask layerMask;
    public float jumpHeight;
    public float speed;
    public bool debug;
	public int horizontalRays;
	public int verticalRays;
    public float groundedRayDistance;

	bool isJumping;
	float normalizedHorizontalSpeed;
	float lastNormalizedHorizontalSpeed;

	bool isRecording;
	List<Vector3> movements;

    Rigidbody2D rb2d;
	List<float> horizontalRaycastYs = new List<float>();
	List<float> verticalRaycastXs = new List<float>();
    private float horizontalRaycastX;
    private float verticalRaycastY;


    void Awake()
	{
		Collider2D c2d = gameObject.GetComponent<Collider2D>();
		rb2d = gameObject.GetComponent<Rigidbody2D>();

		float totalHeight = 2 * c2d.bounds.extents.y;

		float distanceBetweenHorizontalRays = totalHeight / horizontalRays;
		for (var i = 0; i < horizontalRays; i++)
		{
			horizontalRaycastYs.Add(c2d.bounds.extents.y - (distanceBetweenHorizontalRays * i));
		}
		horizontalRaycastX = c2d.bounds.extents.x;

		float totalWidth = 2 * c2d.bounds.extents.x;

		float distanceBetweenVerticalRays = totalWidth / verticalRays;
		for (var i = 0; i < verticalRays; i ++)
		{
			verticalRaycastXs.Add(c2d.bounds.extents.x - (distanceBetweenVerticalRays * i));
		}
		verticalRaycastY = -c2d.bounds.extents.y;
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

		if (normalizedHorizontalSpeed != 0)
		{
			// Get the distance we want to move on the X
			float idealDistance = speed * Time.deltaTime * normalizedHorizontalSpeed;

			// Check how far we can move on the X
			float actualDistance = getMaxMovementOnX(idealDistance > 0, idealDistance);

			// Compensate for direction
			actualDistance = normalizedHorizontalSpeed > 0 ? actualDistance : -actualDistance;
			
			// Flip the sprite appropriately
			if (normalizedHorizontalSpeed != lastNormalizedHorizontalSpeed)
			{
				Vector2 newScale = transform.localScale;
				newScale.x = normalizedHorizontalSpeed < 0 ? -newScale.x : Mathf.Abs(newScale.x);
				transform.localScale = newScale;
			}
			lastNormalizedHorizontalSpeed = normalizedHorizontalSpeed;

			// Move
			transform.position += new Vector3(actualDistance, 0f);
		}
		
		// If we're recording, record our position
		if (isRecording)
			movements.Add(transform.position); // TODO: Store time and stuff
	}

	float getMaxMovementOnX(bool isGoingRight, float maxDistance){
		// Default to however long
		float maxMovement = Mathf.Abs(maxDistance);

		// Get the directions for the rays
		Vector2 rayDir = isGoingRight ? Vector2.right : Vector2.left;

		// Cast one from each origin
		RaycastHit2D ray;
		foreach (float y in horizontalRaycastYs)
		{
			Vector2 origin = new Vector2(isGoingRight ? horizontalRaycastX : -horizontalRaycastX, y);
			ray = Physics2D.Raycast(rb2d.position + origin, rayDir, maxMovement, layerMask);
			DebugRay(rb2d.position + origin, rayDir);
			if (ray && Mathf.Abs(ray.distance) < maxMovement)		
				maxMovement = ray.distance;
		}
		// Return the max movement
		return maxMovement;
	}

	void DebugRay(Vector2 origin, Vector2 dir){
		if (debug){
			Debug.DrawRay(origin, dir);
		}
	}

	bool isGrounded(){

		// Get the directions for the rays
		Vector2 rayDir = Vector2.down;

		// Cast one from each origin
		RaycastHit2D ray;
		foreach (float x in verticalRaycastXs)
		{
			Vector2 origin = new Vector2(x, verticalRaycastY);
			ray = Physics2D.Raycast(rb2d.position + origin, rayDir, groundedRayDistance, layerMask);
			DebugRay(rb2d.position + origin, rayDir);
			if (ray)		
				return true;
		}
		return false;
	}

	void PerformJumps()
	{
		// Reset the velocity if we're not jumping or have just landed.
		if (isJumping && isGrounded()){
			isJumping = false;
			rb2d.velocity = Vector2.zero;
		}

		// Check if we want to jump & can
		if (Input.GetKey(KeyCode.UpArrow) && !isJumping && canJump()){
			// Add a force
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
