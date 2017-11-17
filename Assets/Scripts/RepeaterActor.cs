using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeaterActor : MonoBehaviour {

	public List<Vector3> movements;

	int currentIndex;
	
	void Update () {
		currentIndex++;
		if (movements != null && currentIndex < movements.Count){
			transform.position = movements[currentIndex];
		}else{
			Destroy(gameObject); // TODO: Fancy effect
		}
	}
}
