using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CheckForWallScript : MonoBehaviour
{

	public Rigidbody playerRb;

	RigidbodyFirstPersonController rbController;

	public float knockBackForce = 100f;
	//public float upwardForce = 10f;

	public bool isStun = false;

	public float stunDuration = 1f;
	public float stunCounter = 0f;

	public float originalSpeed;

	void Start ()
	{
		rbController = FindObjectOfType<RigidbodyFirstPersonController> ();

		originalSpeed = rbController.movementSettings.ForwardSpeed;
	}


	void Update ()
	{
		CheckStun ();
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.layer == 10 && this.gameObject.layer == 11) {
	
			Debug.Log ("Hit The Wall");
	
			//playerRb.AddForce (Vector3.back * 1000f * Time.deltaTime, ForceMode.Impulse);
	
			//playerRb.AddRelativeForce (Vector3.back * knockBackForce, ForceMode.VelocityChange);

			//playerRb.velocity += Vector3.back * knockBackForce;

			//playerRb.velocity = -(transform.forward * knockBackForce) + (transform.up * upwardForce);

			//playerRb.velocity = (transform.up * upwardForce);

			playerRb.velocity = -(transform.forward * knockBackForce);

			isStun = true;
	
		}
	}

	void CheckStun ()
	{
		if (isStun) {

			if (stunDuration >= stunCounter) {

				stunCounter += Time.deltaTime;

				rbController.movementSettings.ForwardSpeed = 0f;

			} else {

				stunCounter = 0f;

				rbController.movementSettings.ForwardSpeed = originalSpeed;

				isStun = false;

			}

		}
	}

	/*
	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.layer == 10 && this.gameObject.layer == 11) {

			Debug.Log ("Hit The Wall");

			//playerRb.AddForce (Vector3.back * 1000f * Time.deltaTime, ForceMode.Impulse);

			playerRb.AddRelativeForce (Vector3.back * knockBackForce, ForceMode.Force);

		}
	}
	*/
}
