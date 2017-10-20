using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

	public Transform target;
	public GameObject minion;
	public float spawnDistance;
	public float prevDistance;
	public float remainingDistance;
	public float distance;

	public Transform[] pointList;
	public Vector3 spawnPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetKeyDown(KeyCode.Alpha0))
			//PoolManagerScript.Instance.SpawnMuliple("Surveillance_Drone", spawnPoint, Quaternion.identity, 3, 1.0f);

		if(Input.GetKeyDown(KeyCode.A))
		{
//			distance = Vector3.Distance(transform.position,target.transform.position);
//			if(distance == 4)
//			{
//				spawnPoint = target.transform.position;
//			}
//			else if (distance > 4)
//			{
//				spawnPoint = Vector3.Lerp(transform.position,target.transform.position,CalculateRange(transform.position,target.transform.position,spawnDistance));
//			}
//			else if(distance < 4)
//			{
//				Debug.Log(pointList.Length-2);
//				for(int i = pointList.Length-2; i >= 0; i--)
//				{
//					Debug.Log("bebefore" + distance);
//					prevDistance = distance;
//					distance += Vector3.Distance(target.transform.position,pointList[i].transform.position);
//					if(distance >= 4)
//					{
//						if(distance == 4)
//						{
//							spawnPoint = pointList[i].transform.position;
//							break;
//						}
//						else if(distance > 4)
//						{
//							Debug.Log("before" + distance);
//							distance = spawnDistance - prevDistance;
//							Debug.Log("after" +distance);
//							spawnPoint = Vector3.Lerp(target.transform.position,pointList[i].transform.position,CalculateRange(target.transform.position,pointList[i].transform.position,distance));
//							Debug.Log(Vector3.Distance(transform.position,spawnPoint));
//							//spawnPoint = Vector3.Lerp(pointList[i].transform.position,target.transform.position,CalculateRange(transform.position,target.transform.position,distance));
//							break;
//						}
//					}
//					else
//					{
//						target = pointList[i];
//					}
//				}
//
//			}
			bool horizontal = false;
			SpawnManagerScript.Instance.CalculateSpawnPoint();
			//PoolManagerScript.Instance.Spawn("Surveillance_Drone",SpawnManagerScript.Instance.spawnPoint,Quaternion.identity);

		//	PoolManagerScript.Instance.SpawnMuliple("Surveillance_Drone",SpawnManagerScript.Instance.spawnPoint,Quaternion.identity,3,0.5f,1.5f,horizontal);
			//PoolManagerScript.Instance.SpawnMuliple("Surveillance_Drone",SpawnManagerScript.Instance.spawnPoint,Quaternion.identity,3,0.5f,1.5f,horizontal);
		}
		//transform.Translate(Vector3.left * Time.deltaTime * 1.5f);
	}

	float CalculateRange(Vector3 a, Vector3 b, float distance)
	{
		float range = distance / (a-b).magnitude;
		return range;
	}

}
