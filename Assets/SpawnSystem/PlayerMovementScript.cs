using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript: MonoBehaviour {
	public float movementSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
	}
}
