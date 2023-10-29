using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EndScript : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction interactionAction;

	private void Start()
	{
		playerInput = InputProvider.GetPlayerInput();
		interactionAction = playerInput.actions["Interaction"];
	}
	private void OnTriggerEnter(Collider other)
	{
		UIManager.Instance.interactionBox.text = "Interact to exit Eldoria";
	}

	private void OnTriggerExit(Collider other)
	{
		UIManager.Instance.interactionBox.text = null;
	}

	private void InteractionCheck()
	{
		if(interactionAction.triggered) 
		{
			Debug.Log("exit called");
			Application.Quit();
		}
	}
}
