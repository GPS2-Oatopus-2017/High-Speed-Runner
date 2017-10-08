using System.Collections;
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
	}

	void TakeDamage()
	{
		HealthBarScript.Instance.ResetHealthBar(health);
	}
}
