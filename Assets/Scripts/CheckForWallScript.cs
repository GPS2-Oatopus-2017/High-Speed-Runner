using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CheckForWallScript : MonoBehaviour
{

	public Rigidbody playerRb;

	RigidbodyFirstPersonController rbController;

	public float knockBackForce = 1000f;

	void Start ()
	{
		rbController = FindObjectOfType<RigidbodyFirstPersonController> ();
	}


	void Update ()
	{
		
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.layer == 10 && this.gameObject.layer == 11) {
	
			Debug.Log ("Hit The Wall");
	
			//playerRb.AddForce (Vector3.back * 1000f * Time.deltaTime, ForceMode.Impulse);
	
			playerRb.AddRelativeForce (Vector3.back * knockBackForce, ForceMode.Impulse);
	
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
