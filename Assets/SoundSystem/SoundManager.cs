using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public Sound[] sounds;

	public static SoundManager instance;

	// Use this for initialization
	void Awake () {

		if(instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		foreach (Sound audio in sounds)
		{
			audio.source = gameObject.AddComponent<AudioSource>();
			audio.source.clip = audio.clip;

			audio.source.volume = audio.volume;
			audio.source.pitch = audio.pitch;
			audio.source.loop = audio.loop;
		}
	}
	
	public void Play(string name)
	{
		Sound audio = Array.Find(sounds, sound => sound.name == name);
		if(audio == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		audio.source.Play(); //AudioManager.instance.Play("*insert audioclip's name*");
	}
}
