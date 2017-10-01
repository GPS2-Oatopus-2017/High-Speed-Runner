using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingDroneScript : MonoBehaviour {

	public GameObject player; // Public for now

	public float movementSpeed = 2.0f;
	public float turnSpeed = 4.0f;

	public int chaseMaxDistance = 10; // To be adjusted
	public int chaseMinDistance = 2; // To be adjusted

	public float hoverForce = 90.0f; // To be adjusted
	public float hoverHeight = 3.5f; // To be adjusted

	private Rigidbody huntingDroneRigidbody;

	public GameObject bullet;
	public Transform droneGunHardPoint1;
	public Transform droneGunHardPoint2;
	public float fireRate = 3.0f;
	private float nextFire;
	private bool lastGunHardPoint;

	void Awake()
	{
		huntingDroneRigidbody = GetComponent<Rigidbody>();
	}


	void Start()
	{
		player = GameObject.FindWithTag("Player");

		float randNum = Random.Range(3,6);
		hoverHeight = randNum;

		nextFire = fireRate;
	}


	void Update()
	{
		// Currently chases you without a defined path. Will add following waypoints later when there is a defined path.
		transform.LookAt(player.transform.position);

		if(Vector3.Distance(transform.position, player.transform.position) >= chaseMinDistance)
		{
			transform.position += transform.forward * movementSpeed * Time.deltaTime;

			if(Vector3.Distance(transform.position, player.transform.position) <= chaseMinDistance)
			{
				// Some action here when close to the player
				Debug.Log("Hunting Drone Body Slammed You! -- You Take 1 DMG");
			}
		}

		// For Drone Shooting
		if(Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;

			if(lastGunHardPoint == true)
			{
				Instantiate(bullet, droneGunHardPoint1.position, droneGunHardPoint1.rotation);
				lastGunHardPoint = false;
			}
			else
			{
				Instantiate(bullet, droneGunHardPoint2.position, droneGunHardPoint2.rotation);
				lastGunHardPoint = true;
			}
		}
	}


	void FixedUpdate()
	{
		// For Drone Hovering
		Ray hoverRay = new Ray (transform.position, -transform.up);
		RaycastHit hoverHit;

		if(Physics.Raycast(hoverRay, out hoverHit, hoverHeight))
		{
			float propotionalHeight = (hoverHeight - hoverHit.distance) / hoverHeight;
			Vector3 appliedHoverForce = Vector3.up * propotionalHeight * hoverForce;
			huntingDroneRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
		}
	}
}
