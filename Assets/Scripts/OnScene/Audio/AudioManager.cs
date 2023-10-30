using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;
	public AudioClip[] backgroundMusicClips;
	private AudioSource audioSource;
	private int currentClipIndex = 0;

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			audioSource = gameObject.GetComponent<AudioSource>();
		}
		else
		{
			Destroy(gameObject);
		}

		PlayNextBGM();

	}

	private void PlayNextBGM()
	{
		if(currentClipIndex < backgroundMusicClips.Length) 
		{
			audioSource.clip = backgroundMusicClips[currentClipIndex];
			audioSource.Play();
			currentClipIndex++;

			Invoke("PlayNextBGM", audioSource.clip.length);
		}
		else
		{
			currentClipIndex = 0;
			PlayNextBGM();
		}
	}
}
