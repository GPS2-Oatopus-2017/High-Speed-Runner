using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	//Pyramind's
//	public GameObject audiosourcePrefab;

	//Brackeys
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

		//Brackeys
		foreach (Sound audio in sounds)
		{
			audio.source = gameObject.AddComponent<AudioSource>();
			audio.source.clip = audio.clip;

			audio.source.volume = audio.volume;
			audio.source.pitch = audio.pitch;
			audio.source.loop = audio.loop;
			audio.source.spatialBlend = audio.spatialBlend;
		}
	}
		
	//Brackeys
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

	//Pyramind's
//	public void PlayAudio(AudioClip clip, GameObject objecttoplayOn, float volume, float pitch, bool loop = false, float spatialBlend = 1f)
//	{
//		//AudioSource.PlayClipAtPoint(clip, objecttoplayOn.transform.position); //AudioManager.instance.playatGo("clip name", gameObject);
//		AudioSource myaudioSource = Instantiate(audiosourcePrefab).GetComponent<AudioSource>();
//		myaudioSource.Play(); //AudioManager.instance.PlayAudio("clip name", gameObject, 0f, 0f, 0f);
//		myaudioSource.name = clip.name + objecttoplayOn.name;
//		myaudioSource.gameObject.transform.position = objecttoplayOn.transform.position;
//		myaudioSource.gameObject.transform.parent = objecttoplayOn.gameObject.transform;
//		myaudioSource.clip = clip;
//		myaudioSource.volume = volume;
//		myaudioSource.pitch = pitch; //Add Random.Range(0.8f, 1.2f)
//		myaudioSource.loop = loop;
//		myaudioSource.spatialBlend = spatialBlend;
//
//		Destroy(myaudioSource.gameObject, myaudioSource.clip.length);
//	}

	public void StopAudio(string soundtostopName)
	{
		GameObject soundtostopObject = GameObject.Find(soundtostopName);
		soundtostopObject.GetComponent<AudioSource>().Stop();

		Destroy(soundtostopObject); //AudioManager.instance.StopAudio("object name".name + gameObject.name);
	}
}
