using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManagerScript : MonoBehaviour {

	private static SpawnManagerScript mInstance;
	public static SpawnManagerScript Instance
	{
		get { return mInstance; }
	}

	public Transform player;
	public int reputation;
	public float countDownTimer;
	public float spawnTime;
	public int sdCount;
	public int hdCount;

	//calculation
	public bool isHorizontal;
	public float spawnDistance = 4.0f;
	public Transform target; //change to waypoints
	public float prevDistance;
	public float distance;
	public Vector3 spawnPoint;
	public float offsetY = 1.0f;
	public float offset = 1.0f;
	public int currentSpawnIndex;

	void Awake()
	{
		if(mInstance == null) mInstance = this;
		else if(mInstance != this) Destroy(this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		if(WaypointManagerScript.Instance.playerDirection == Direction.North || WaypointManagerScript.Instance.playerDirection == Direction.South)
		{
			isHorizontal = false;
		}
		else
		{
			isHorizontal = true;
		}
		//currentSpawnIndex 
		reputation = ReputationManagerScript.Instance.lastRep;
		if(reputation >= 1 && reputation == ReputationManagerScript.Instance.currentRep)
		{
			countDownTimer += Time.deltaTime;
		}
		else
		{
			countDownTimer = 0;
		}

		if(countDownTimer >= spawnTime)
		{
			countDownTimer = 0;
			CalculateSpawnPoint();
			if(reputation == 1)
			{
				sdCount+=1;
				hdCount+=1;
				PoolManagerScript.Instance.Spawn("Surveillance_Drone",spawnPoint,Quaternion.identity);
				PoolManagerScript.Instance.Spawn("Hunting_Droid",spawnPoint,Quaternion.identity);
				TimelineScript.Instance.CreateEnemyIcon("Surveillance_Drone", 1);
				TimelineScript.Instance.CreateEnemyIcon("Hunting_Droid", 1);
			}
			else if(reputation == 2)
			{
				hdCount+=2;
				PoolManagerScript.Instance.SpawnMuliple("Hunting_Droid",spawnPoint,Quaternion.identity,2,offsetY,offset,isHorizontal);
				TimelineScript.Instance.CreateEnemyIcon("Hunting_Droid", 2);
			}
			else if(reputation == 3)
			{
				sdCount+=1;
				hdCount+=2;
				PoolManagerScript.Instance.Spawn("Surveillance_Drone",spawnPoint,Quaternion.identity);
				ApplyOffsetVertically();
				PoolManagerScript.Instance.SpawnMuliple("Hunting_Droid",spawnPoint,Quaternion.identity,2,offsetY,offset,isHorizontal);
				TimelineScript.Instance.CreateEnemyIcon("Surveillance_Drone", 1);
				TimelineScript.Instance.CreateEnemyIcon("Hunting_Droid", 2);
			}
			else if(reputation == 4)
			{
				sdCount+=1;
				hdCount+=3;
				PoolManagerScript.Instance.Spawn("Surveillance_Drone",spawnPoint,Quaternion.identity);
				ApplyOffsetVertically();
				for(int i=0; i<3; i++)
				{
					PoolManagerScript.Instance.Spawn("Hunting_Droid",spawnPoint,Quaternion.identity);
				}
				TimelineScript.Instance.CreateEnemyIcon("Surveillance_Drone", 1);
				TimelineScript.Instance.CreateEnemyIcon("Hunting_Droid", 3);
			}
			else if(reputation == 5)
			{
				sdCount+=3;
				hdCount+=3;
				for(int i=0; i<3; i++)
				{
					PoolManagerScript.Instance.Spawn("Surveillance_Drone",spawnPoint,Quaternion.identity);
				}
				ApplyOffsetVertically();
				for(int i=0; i<3; i++)
				{
					PoolManagerScript.Instance.Spawn("Hunting_Droid",spawnPoint,Quaternion.identity);
				}
				TimelineScript.Instance.CreateEnemyIcon("Surveillance_Drone", 3);
				TimelineScript.Instance.CreateEnemyIcon("Hunting_Droid", 3);
			}
		}
	}

	void ApplyOffsetVertically()
	{
		spawnPoint.y += offsetY;
	}

	public float CalculateRange(Vector3 a, Vector3 b, float distance)
	{
		float range = distance / (a-b).magnitude;
		return range;
	}

	public void CalculateSpawnPoint()
	{
		target = WaypointManagerScript.Instance.tracePlayerNodes[WaypointManagerScript.Instance.tracePlayerNodes.Count-1].transform;
		distance = Vector3.Distance(player.position,target.transform.position);
		if(distance == spawnDistance)
		{
			spawnPoint = target.transform.position;
			currentSpawnIndex = WaypointManagerScript.Instance.tracePlayerNodes.Count-1;
		}
		else if (distance > spawnDistance)
		{
			spawnPoint = Vector3.Lerp(player.position,target.transform.position,CalculateRange(player.position,target.transform.position,spawnDistance));
			currentSpawnIndex = WaypointManagerScript.Instance.tracePlayerNodes.Count-1;
		}
		else if(distance < spawnDistance)
		{
			for(int i = WaypointManagerScript.Instance.tracePlayerNodes.Count-2; i >= 0; i--)
			{
				prevDistance = distance;
				distance += Vector3.Distance(target.transform.position,WaypointManagerScript.Instance.tracePlayerNodes[i].transform.position);
				if(distance >= spawnDistance)
				{
					if(distance == spawnDistance)
					{
						spawnPoint = WaypointManagerScript.Instance.tracePlayerNodes[i].transform.position;
						currentSpawnIndex = i;
						break;
					}
					else if(distance > spawnDistance)
					{
						distance = spawnDistance - prevDistance;
						spawnPoint = Vector3.Lerp(target.transform.position,WaypointManagerScript.Instance.tracePlayerNodes[i].transform.position,CalculateRange(target.transform.position,WaypointManagerScript.Instance.tracePlayerNodes[i].transform.position,distance));
						currentSpawnIndex = i;
						break;
					}
				}
				else
				{
					target = WaypointManagerScript.Instance.tracePlayerNodes[i].transform;
				}
			}

		}
	}
}
