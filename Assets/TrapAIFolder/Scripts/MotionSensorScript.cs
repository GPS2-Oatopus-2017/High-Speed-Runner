using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionSensorScript : MonoBehaviour {

	public GameObject player; // Public for now
	public GameObject electricWall;

	public bool isActive;
	
	public float alertDistance = 3; // To be adjusted

	void Start() 
	{
		player = GameObject.FindWithTag("Player");

		isActive = true;
	}


	void Update()
	{
		if(Vector3.Distance(transform.position, player.transform.position) <= alertDistance)
		{
			electricWall.SetActive(true);
			isActive = false;
		}
	}
}
