using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordingOverlay : MonoBehaviour {

	public Text text;

	public float timeBetweenFlashes;

	float timeSinceFlash;
	
	string prevText;

	// Update is called once per frame
	void Update () {
		timeSinceFlash += Time.deltaTime;

		if (timeSinceFlash >= timeBetweenFlashes)
		{
			if (text.text == "")
				text.text = prevText;
			else{
				prevText = text.text;
				text.text = "";
			}
			timeSinceFlash = 0f;
		}
	}
}
