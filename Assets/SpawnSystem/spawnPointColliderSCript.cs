using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPointColliderSCript : MonoBehaviour {

	public Transform forward;
	public Transform backward;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Player")
		{
			SpawnManagerScript.Instance.playerLastLocation = this.transform;
		}
	}
}
