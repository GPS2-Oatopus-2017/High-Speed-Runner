using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceDroneScript : MonoBehaviour {

	public GameObject player; // Public for now

	public float movementSpeed = 11.0f;
	public float turnSpeed = 8.0f;

	public float alertDistance = 12.0f; // To be adjusted
	public float safeDistance = 26.0f; // To be adjusted

	public float hoverForce = 90.0f; // To be adjusted
	public float hoverHeight = 3.5f; // To be adjusted

	private Rigidbody surveillanceDroneRigidbody;

	public bool hasBeenDetected;

	void Awake()
	{
		surveillanceDroneRigidbody = GetComponent<Rigidbody>();
	}


	void Start()
	{
		player = GameObject.FindWithTag("Player");

		float randNum = Random.Range(3,6);
		hoverHeight = randNum;

		hasBeenDetected = false;
	}


	void Update()
	{
		playerDetection();
		surveillanceDroneMainFunctions();
	}


	void FixedUpdate()
	{
		droneHoveringFunction();
	}


	void playerDetection()
	{
		if(Vector3.Distance(transform.position, player.transform.position) <= alertDistance)
		{
			hasBeenDetected = true;
		}
	}


	void surveillanceDroneMainFunctions()
	{
		transform.LookAt(player.transform.position);

		if(hasBeenDetected == true)
		{
			if(Vector3.Distance(transform.position, player.transform.position) >= safeDistance)
			{
				hasBeenDetected = false;

				Debug.Log("Surveillance Drone No Longer Following Player (More Than safeDistance)");
			}
			else
			{
				transform.position += transform.forward * movementSpeed * Time.deltaTime;
			}
		}
		else
		{
			surveillanceDroneRigidbody.velocity = surveillanceDroneRigidbody.velocity * 0.9f;
		}
	}


	void droneHoveringFunction()
	{
		Ray hoverRay = new Ray (transform.position, -transform.up);
		RaycastHit hoverHit;

		if(Physics.Raycast(hoverRay, out hoverHit, hoverHeight))
		{
			float propotionalHeight = (hoverHeight - hoverHit.distance) / hoverHeight;
			Vector3 appliedHoverForce = Vector3.up * propotionalHeight * hoverForce;
			surveillanceDroneRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
		}
	}
}
