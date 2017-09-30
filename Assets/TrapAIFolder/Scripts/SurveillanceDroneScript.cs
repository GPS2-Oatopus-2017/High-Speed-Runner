using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceDroneScript : MonoBehaviour {

	public GameObject player; // Public for now

	public float movementSpeed = 4.0f;
	public float turnSpeed = 8.0f;

	public int chaseMaxDistance = 10; // To be adjusted
	public int chaseMinDistance = 2; // To be adjusted

	public float heightFromGround;

	void Start()
	{
		player = GameObject.FindWithTag("Player");

		int randNum = Random.Range(7,11);
		heightFromGround = randNum;
	}


	void Update()
	{

		Transform playerPOS = player.transform;
		transform.LookAt(playerPOS);

		if(Vector3.Distance(transform.position, playerPOS.position) >= chaseMinDistance)
		{
			transform.position += transform.forward * movementSpeed * Time.deltaTime;

			if(Vector3.Distance(transform.position, playerPOS.position) <= chaseMinDistance)
			{
				// Some Action Here
			}
		}
	}


}
