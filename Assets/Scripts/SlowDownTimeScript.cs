using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownTimeScript : MonoBehaviour
{

	public float slowTime = 0.25f;

	public float originalTime;

	void Start ()
	{
		originalTime = Time.timeScale;
	}

	void Update ()
	{
		
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Player" && this.gameObject.layer == 12) {

			Time.timeScale = slowTime;

			Debug.Log ("IN Slow Motion");

		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Player" && this.gameObject.layer == 12) {

			Time.timeScale = originalTime;

			Debug.Log ("OUT Slow Motion");
		}
	}
}
