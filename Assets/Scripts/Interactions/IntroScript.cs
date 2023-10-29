using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
	public GameObject introText;

	private void OnTriggerEnter(Collider other)
	{
		introText.SetActive(true);
	}

	private void OnTriggerExit(Collider other)
	{
		introText.SetActive(false);
	}
}
