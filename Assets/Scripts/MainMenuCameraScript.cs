using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraScript : MonoBehaviour {

	public Vector3 startPoint;

	public Vector3 endPoint;

	public float speed;

	void Update () {
		Vector3 newPos = transform.position;

		if (newPos == endPoint)
		{
			newPos = startPoint;
		}else{
			newPos.x += speed;
			if (newPos.x >= endPoint.x)
			{
				newPos = endPoint;
			}
		}

		transform.position = newPos;
	}
}
