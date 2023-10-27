using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ColorKD
{
	Red,
	Yellow,
	Blue
}

public class BaseInteractable : MonoBehaviour
{
	protected bool playerInRange = false;

	private PlayerInput playerInput;
	private InputAction interactionAction;

	protected virtual void Start()
	{
		playerInput = InputProvider.GetPlayerInput();
		interactionAction = playerInput.actions["Interaction"];
	}

	protected virtual void OnDestroy()
	{
		
	}
	protected void Update()
	{
		if (playerInRange)
		{
			if (interactionAction.WasPerformedThisFrame())
			{
				Debug.Log("interaction performed");
				OnInteract();
			}
		}
	}

	protected virtual void OnInteract()
	{
	}

	protected virtual void InteractableTriggerAction()
	{
	}

	protected void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			playerInRange = true;
		}
		InteractableTriggerAction();
	}
	protected void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			playerInRange = false;
		}
	}
}
