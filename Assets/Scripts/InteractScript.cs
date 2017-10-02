using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class InteractScript : MonoBehaviour
{

	RaycastHit hit;
	Ray ray;
	UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController controller;

	void Start ()
	{
		
	}

	void Update ()
	{
		CheckInteract ();
	}

	//When left mouse button is pressed, shoot out a ray cast from screen to pointer.//
	//If the player is within the radius of the object, it will go towards it.//

	void CheckInteract ()
	{
		if (Input.GetMouseButton (0) || Input.touchCount > 0) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 100.0f)) {
				Debug.DrawRay (transform.position, hit.transform.position, Color.red);
				Debug.Log (hit.transform.name);

			}
		}
	}
}
