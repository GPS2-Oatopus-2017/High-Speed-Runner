using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMLoader : MonoBehaviour {

	void Start()
	{
		if(SceneManager.GetActiveScene().name == "MainMenu")
		{
			SoundManager.instance.Play("Main Menu BGM");
		}

		if(SceneManager.GetActiveScene().name == "WaypointScene")
		{
			SoundManager.instance.Play("Test BGM");
		}
	}

	void Update()
	{
		if(SceneManager.GetActiveScene().name != "MainMenu")
		{
			SoundManager.instance.Stop("Main Menu BGM");
		}

		if(SceneManager.GetActiveScene().name != "WaypointScene")
		{
			SoundManager.instance.Stop("Test BGM");
		}
	}
}
