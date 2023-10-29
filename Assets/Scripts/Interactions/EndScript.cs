using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		UIManager.Instance.interactionBox.text = "Interact to exit Eldoria";
	}

	private void OnTriggerExit(Collider other)
	{
		UIManager.Instance.interactionBox.text = null;
	}
}
