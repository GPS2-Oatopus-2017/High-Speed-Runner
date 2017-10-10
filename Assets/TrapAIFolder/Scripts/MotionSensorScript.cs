using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionSensorScript : MonoBehaviour {

	public GameObject player; // Public for now
	//public GameObject electricWall;

	public bool isActive;
	
	public float alertDistance = 6; // To be adjusted

	void Start() 
	{
		player = GameObject.FindWithTag("Player");

		isActive = true;
	}


	void Update()
	{
		if(Vector3.Distance(transform.position, player.transform.position) <= alertDistance && isActive)
		{
			//electricWall.SetActive(true);
			isActive = false;
			SpawnManagerScript.Instance.CalculateSpawnPoint();
			PoolManagerScript.Instance.SpawnMuliple("Hunting_Droid",SpawnManagerScript.Instance.spawnPoint,Quaternion.identity,2,0,3.5f,SpawnManagerScript.Instance.isHorizontal);
			Debug.Log(SpawnManagerScript.Instance.isHorizontal);
			Debug.Log("Touche");
		}
	}
}
