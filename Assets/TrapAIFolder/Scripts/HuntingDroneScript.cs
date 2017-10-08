using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingDroneScript : MonoBehaviour {

	public GameObject player; // Public for now
	public GameObject enemyAttackIndicator;

	private Vector3 target;
	public float targetOffset;

	public float movementSpeed = 9.0f;
	public float turnSpeed = 4.0f;

	public float safeDistance = 26.0f; // To be adjusted

	public float hoverForce = 90.0f; // To be adjusted
	public float hoverHeight = 3.5f; // To be adjusted

	private Rigidbody huntingDroneRigidbody;

	public GameObject bullet;
	public Transform droneGunHardPoint1;
	public Transform droneGunHardPoint2;
	public float fireRate = 3.0f;
	public float fireIndication = 1.5f;
	private float nextFire;
	private bool lastGunHardPoint;

	public bool isWithinRange;

	void Awake()
	{
		huntingDroneRigidbody = GetComponent<Rigidbody>();
	}


	void Start()
	{
		player = GameObject.FindWithTag("Player");

		target = player.transform.position + player.transform.forward * targetOffset;

		float randNum = Random.Range(3,6);
		hoverHeight = randNum;

		nextFire = fireRate;
	}


	void Update()
	{
		huntingDroneMainFunctions();
	}


	void FixedUpdate()
	{
		droneHoveringFunction();
	}


	void huntingDroneMainFunctions()
	{
		transform.LookAt(player.transform.position);

		if(Vector3.Distance(transform.position, player.transform.position) >= safeDistance)
		{
			isWithinRange = false;
			huntingDroneRigidbody.velocity = huntingDroneRigidbody.velocity * 0.9f;

			Debug.Log("Hunting Drone No Longer Chasing Player (More Than safeDistance)");
		}
		else
		{
			isWithinRange = true;

			transform.position += transform.forward * movementSpeed * Time.deltaTime;
		}
			
		if(isWithinRange == true)
		{
			if(Time.time > fireIndication)
			{
				fireIndication = Time.time + fireRate;

				target = player.transform.position + (player.transform.forward * targetOffset);
				GameObject indicator = Instantiate(enemyAttackIndicator, new Vector3(target.x, 0.1f, target.z), Quaternion.LookRotation(enemyAttackIndicator.transform.up));
				Destroy(indicator, 2f);
			}

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
	}
	void droneHoveringFunction()
	{
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
