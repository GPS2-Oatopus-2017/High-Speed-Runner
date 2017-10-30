﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingDroneScript : MonoBehaviour {

	public GameObject player;
	public GameObject enemyAttackIndicator;
	public Vector3 chasingPosition;

	private Vector3 target;
	public float targetOffset;

	public float movementSpeed = 12.5f;
	public float turnSpeed = 4.0f;

	public float safeDistance = 50.0f;

	public float hoverForce = 90.0f;
	public float hoverHeight = 3.5f;

	private Rigidbody huntingDroneRigidbody;

	public GameObject bullet;
	public Transform droneGunHardPoint;
	public float fireRate = 4.0f;
	public float fireIndication = 1.5f;
	private float nextFire;
	private bool lastGunHardPoint;

	public bool isWithinRange;

	public int currentPoint = 0;

	public float distanceOfPlayer;

	void Awake()
	{
		player = GameObject.FindWithTag("Player");
		huntingDroneRigidbody = GetComponent<Rigidbody>();
	}


	void Start()
	{
		SpawnManagerScript.Instance.CalculateSpawnPoint();
		currentPoint = SpawnManagerScript.Instance.currentSpawnIndex + 1;
		target = player.transform.position + player.transform.forward * targetOffset;

		float randNum = Random.Range(3,6);
		hoverHeight = randNum;

		nextFire = fireRate;
	}


	void Update()
	{
		huntngDroneChaseFunctions();
		huntingDroneMainFunctions();

		if(ReputationManagerScript.Instance.currentRep == 0)
		{
			PoolManagerScript.Instance.Despawn(this.gameObject);
		}

		distanceOfPlayer = Vector3.Distance(transform.position, player.transform.position);
	}


	void FixedUpdate()
	{
		droneHoveringFunction();
	}


	void huntngDroneChaseFunctions()
	{
		if(Vector2.Distance(new Vector2(chasingPosition.x, chasingPosition.z), new Vector2(transform.position.x, transform.position.z)) <= 0.1f)
		{
			if(currentPoint < WaypointManagerScript.Instance.tracePlayerNodes.Count)
			{
				currentPoint++;
			}
		}

		Transform chasingTrans = player.transform;

		if(currentPoint < WaypointManagerScript.Instance.tracePlayerNodes.Count)
		{
			chasingTrans = WaypointManagerScript.Instance.tracePlayerNodes[currentPoint].transform;
		}

		chasingPosition = chasingTrans.position;
		chasingPosition.y = transform.position.y;
	}


	void huntingDroneMainFunctions()
	{
		transform.LookAt(chasingPosition);

		if(Vector3.Distance(transform.position, player.transform.position) >= safeDistance)
		{
			isWithinRange = false;
			huntingDroneRigidbody.velocity = huntingDroneRigidbody.velocity * 0.9f;

			//Debug.Log("Hunting Drone No Longer Chasing Player (More Than safeDistance)");
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
				GameObject indicator = Instantiate(enemyAttackIndicator, new Vector3(target.x, 0.1f, target.z), enemyAttackIndicator.transform.rotation);
				Destroy(indicator, 2f);
			}

			if(Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				Instantiate(bullet, droneGunHardPoint.position, droneGunHardPoint.rotation);

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
