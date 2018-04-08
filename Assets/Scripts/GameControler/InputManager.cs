using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public Camera MainCamera;
	GameObject selectedGO;

	void Update()
	{
		if (Input.GetMouseButtonDown(0)) { // Left click
			Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				Transform objectHit = hit.transform;
				Debug.Log("it's a f*cking " + objectHit.tag + "!");
			}
		}
	}
}

