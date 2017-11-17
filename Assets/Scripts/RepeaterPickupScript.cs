using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeaterPickupScript : MonoBehaviour {
    private GlobalManagerScript manager;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GlobalManagerScript>();
	}

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player"){
			other.gameObject.GetComponent<RepeaterScript>().enabled = true;
			manager.ShowRepeaterTutorial();
			Destroy(gameObject);
		}
	}
}
