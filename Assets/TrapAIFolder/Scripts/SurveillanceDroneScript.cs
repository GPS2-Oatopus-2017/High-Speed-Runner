using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceDroneScript : MonoBehaviour {

	public GameObject player; // Public for now
	public Vector3 chasingPosition; // Public for now

	public float movementSpeed = 11.0f;
	public float turnSpeed = 8.0f;

	public float alertDistance = 12.0f; // To be adjusted
	public float safeDistance = 26.0f; // To be adjusted

	public float hoverForce = 90.0f; // To be adjusted
	public float hoverHeight = 3.5f; // To be adjusted

	private Rigidbody surveillanceDroneRigidbody;

	public bool hasBeenDetected;
	public bool isSpawned;

	public int currentPoint = 0; 

	void Awake()
	{
		player = GameObject.FindWithTag("Player");
		surveillanceDroneRigidbody = GetComponent<Rigidbody>();
	}


	void Start()
	{
		//player = GameObject.FindWithTag("Player");
		float randNum = Random.Range(3,6);
		hoverHeight = randNum;
		currentPoint = SpawnManagerScript.Instance.currentSpawnIndex;
		hasBeenDetected = false;
	}


	void Update()
	{
		if(!hasBeenDetected)
		{
			playerDetection();
		}
		else if(isSpawned)
		{
			if(ReputationManagerScript.Instance.currentRep == 0)
			{
				PoolManagerScript.Instance.Despawn(this.gameObject);
			}
		}
		surveillanceDroneChaseFunctions();
		surveillanceDroneMainFunctions();
	}


	void FixedUpdate()
	{
		droneHoveringFunction();
	}


	void playerDetection()
	{
		if(Vector3.Distance(transform.position, player.transform.position) <= alertDistance && WaypointManagerScript.Instance.tracePlayerNodes.Count > 0)
		{
			hasBeenDetected = true;
			Debug.Log("Touche");
			//SpawnFunction
			SpawnManagerScript.Instance.CalculateSpawnPoint();
			currentPoint = SpawnManagerScript.Instance.currentSpawnIndex + 1;
			if(!isSpawned)
				PoolManagerScript.Instance.Spawn("Hunting_Droid",SpawnManagerScript.Instance.spawnPoint,Quaternion.identity);
			if(ReputationManagerScript.Instance.currentRep == 0)
			{
				Debug.Log("Hi");
				ReputationManagerScript.Instance.currentRep += 1;
			}
		}
	}

	void surveillanceDroneChaseFunctions()
	{
		if(Vector2.Distance(new Vector2(chasingPosition.x, chasingPosition.z), new Vector2(transform.position.x, transform.position.z)) <= 0.1f)
		{
			if(currentPoint < WaypointManagerScript.Instance.tracePlayerNodes.Count)
				currentPoint++;
		}

		Transform chasingTrans = player.transform;

		if(currentPoint < WaypointManagerScript.Instance.tracePlayerNodes.Count)
		{
			chasingTrans = WaypointManagerScript.Instance.tracePlayerNodes[currentPoint].transform;
		}

		chasingPosition = chasingTrans.position;
		chasingPosition.y = transform.position.y;
	}

	void surveillanceDroneMainFunctions()
	{
		transform.LookAt(chasingPosition);

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
