using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour {

	public GameObject anotherLevel;
	public GameObject level;
	public GameObject fence;
	public bool isOn;

	// Use this for initialization
	void Start () {
		isOn = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha0))
			TurnLevel();
		if(!isOn)
		{
			fence.SetActive(true);

		}
		else {
			fence.SetActive(false);
		}
	}

	public void TurnLevel()
	{
		if(!isOn)
		{
			level.transform.Rotate(new Vector3(1.0f,0f,0f), 90.0f);
			anotherLevel.transform.Rotate(new Vector3(1.0f,0f,0f), 90.0f);
			isOn = true;
		}
		else if(isOn)
		{
			level.transform.Rotate(new Vector3(1.0f,0f,0f), -90.0f);
			anotherLevel.transform.Rotate(new Vector3(1.0f,0f,0f), -90.0f);
			isOn = false;
		}
	}
}
