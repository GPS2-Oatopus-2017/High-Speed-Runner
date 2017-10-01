using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionSensorScript : MonoBehaviour {

	public GameObject electricWall;

	public bool isActive;

	void Start() 
	{
		isActive = true;
	}
		
	void OnCollisionEnter(Collision other)
	{
		if(isActive == true)
		{
			if(other.collider.tag == "Player")
			{
				electricWall.SetActive(true);
				isActive = false;
			}
		}
	}
}
