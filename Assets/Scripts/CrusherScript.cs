using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherScript : MonoBehaviour {

	public Vector3 moveDirection;

	public Vector3 endPoint;

	public float speed;

	bool isCrushing = false;

	void Update()
	{
		if (isCrushing){

			// TODO: Some sort of smoothing?
			Vector3 newLoc = transform.position + moveDirection * speed * Time.deltaTime;		
			
			newLoc.x = moveDirection.x >= 0 ? Math.Min(newLoc.x, endPoint.x) : Math.Max(newLoc.x, endPoint.x);
			newLoc.y = Math.Min(newLoc.y, endPoint.y);
			newLoc.z = Math.Min(newLoc.z, endPoint.z);

			if (newLoc == endPoint){
				Debug.Log("Finished Crushing (" + gameObject + ")");
				transform.position = endPoint;
				isCrushing = false;
			}else{
				transform.position = newLoc;
			}
		}
	}

    internal void startCrushing()
    {
		isCrushing = true;
		Debug.Log("Started crushing (" + gameObject + ")");
		// TODO: Change appearance?
    }
}
