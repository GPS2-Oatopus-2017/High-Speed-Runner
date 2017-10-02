using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class InteractScript : MonoBehaviour
{

	RaycastHit hit;
	Ray ray;
	UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController controller;
	PlayerCoreController player;

	public float rayDistance = 10f;

	GameObject lightObject;
	Light lightSet;

	void Awake ()
	{
		player = GetComponent<PlayerCoreController> ();
		lightObject = GameObject.Find ("Directional Light");
		lightSet = FindObjectOfType<Light> ();
	}

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
		if (Input.GetMouseButtonDown (0) || Input.touchCount > 0) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, rayDistance)) {
				Debug.DrawRay (transform.position, hit.transform.position, Color.red);
				Debug.Log (hit.transform.name);
				if (hit.transform.tag == "Interactable") {
					player.RotateTowards (hit.transform.position);
				}
				if (hit.transform.tag == "MountainDew") {
					//lightObject.transform.rotation = Quaternion.Euler (20f, -90f, lightObject.transform.rotation.z);
					//lightObject.transform.rotation = Quaternion.Lerp (lightObject.transform.rotation, Quaternion.identity, Time.deltaTime);
					lightSet.color = Random.ColorHSV (0f, 1f, 0f, 1f, 0f, 1f, 0f, 1f);
				}
			}
		}
	}
}
