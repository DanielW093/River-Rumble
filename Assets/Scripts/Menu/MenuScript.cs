using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Toggle MusicToggle;
	public Toggle AudioToggle;
	public AudioSource Click;

	public void StartGame()
	{
		Click.Play();
		SceneManager.LoadScene("GameScene");
	}

	public void ToggleMusic()
	{
		Click.Play();
		AudioSetting.musicOn = !MusicToggle.isOn;
	}

	public void ToggleAudio()
	{
		if(AudioToggle.isOn)
			AudioListener.volume = 1.0f;
		else
			AudioListener.volume = 0.0f;
	}
}
