using UnityEngine;
using System.Collections;

public class AudioSetting : MonoBehaviour {

	public static bool musicOn = true;

	public AudioSource music;

	void Update()
	{
		music.enabled = musicOn;
	}
}
