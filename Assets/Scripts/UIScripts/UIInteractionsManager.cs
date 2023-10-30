using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInteractionsManager : MonoBehaviour
{
	[SerializeField] GameObject mainMenuPanel;
	[SerializeField] GameObject settingsPanel;

	[SerializeField] Slider BGMVolumeSlider;
	AudioSource audioSource;

	private void Awake()
	{
		audioSource = FindObjectOfType<AudioSource>();
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Confined;
	}

	public void ChangeToSceneWithIndex(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}

	public void SwitchToSettings()
	{
		mainMenuPanel?.SetActive(false);

		settingsPanel?.SetActive(true);
	}

	public void BackToMainMenu()
	{
		settingsPanel?.SetActive(false);

		mainMenuPanel?.SetActive(true);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void UpdateBGMVolume()
	{
		if(BGMVolumeSlider != null && audioSource != null)
		{
			audioSource.volume = BGMVolumeSlider.value;
		}
	}

}
