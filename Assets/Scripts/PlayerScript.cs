﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public int health = 3;
	int temp;
	public static PlayerScript Instance;


	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		temp = health;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(health != temp)
		{
			TakeDamage();
			temp = health;
		}

		if(Input.touchCount > 0)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 10000f))
			{
				Debug.Log("Hit");
				if(hit.collider.tag == "TrapTag")
				{
					hit.collider.GetComponent<MotionSensorScript>().isActive = false;
				}
			}
		}
	}

	void TakeDamage()
	{
		HealthBarScript.Instance.ResetHealthBar(health);
	}
		
}
