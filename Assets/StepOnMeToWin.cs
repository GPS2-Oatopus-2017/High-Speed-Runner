using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepOnMeToWin : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			GetComponent<ChangeSceneScript>().ChangeScenes(0);
		}
	}
}
