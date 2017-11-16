using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {

	public float speed = 10f;
	public float jumpHeight;
	public LayerMask layerMask;
	public float lerpAmount;

	Vector2 topLeft;
	Vector2 topRight;
	Vector2 botLeft;
	Vector2 botRight;


	Rigidbody2D rb2d;
	Collider2D c2d;

	void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		c2d = GetComponent<Collider2D>();

		topLeft = new Vector2(-c2d.bounds.extents.x, c2d.bounds.extents.y);
		topRight = new Vector2(c2d.bounds.extents.x, c2d.bounds.extents.y);
		botLeft = new Vector2(-c2d.bounds.extents.x, -c2d.bounds.extents.y);
		botRight = new Vector2(c2d.bounds.extents.x, -c2d.bounds.extents.y);

	}

	void Update () {
		UpdatePosition ();
	}

	void UpdatePosition() { 
		// Get where to move on each axis
		float horizontal = Input.GetAxis ("Horizontal") * Time.deltaTime * speed;
		Vector3 deltaMovement = getDeltaMovement(horizontal);

		// Add these values to the position
		transform.position = Vector2.Lerp(transform.position, transform.position + deltaMovement, lerpAmount);
	}

	Vector2 getDeltaMovement(float horizontal) {
		// Cast rays from the top and bottom in the direction we're going with a max distance of however far we're travelling.
		float maxXMovement = horizontal * speed * Time.deltaTime;
		Vector2 deltaMovement = new Vector2(maxXMovement, 0f);

		if (Input.GetKeyDown(KeyCode.Space) && isOnFloor()){
			rb2d.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
		}

		// Return it, compensating for direction
		return deltaMovement;
	}


	bool isOnFloor(){
		// Cast two rays downwards from each edge
		RaycastHit2D leftRay = Physics2D.Raycast(botLeft + rb2d.position, Vector2.down, 0.1f, layerMask);
		RaycastHit2D rightRay = Physics2D.Raycast(botRight + rb2d.position, Vector2.down, 0.1f, layerMask);

		// If either hit, return true
		return leftRay.transform || rightRay.transform;
	}

}
