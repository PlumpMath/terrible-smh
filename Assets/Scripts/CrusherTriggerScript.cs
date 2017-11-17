using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherTriggerScript : MonoBehaviour {

	public List<Transform> wallsToCrush;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player"){
			foreach (Transform t in wallsToCrush){
				t.GetComponent<CrusherScript>().startCrushing();
			}
			Destroy(this);
		}
	}
}
