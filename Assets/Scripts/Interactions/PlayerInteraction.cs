using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction interactionAction;

	[SerializeField] private Transform interactionPoint;
	[SerializeField] private float detectionDistance;
	private NPCInteractedScript npcInteractedScript;
	private GameObject interactingNPC;
	private GameObject textArea;
	private CharacterController playerCharController;

	private void Start()
	{
		playerInput = InputProvider.GetPlayerInput();
		playerCharController = GetComponent<CharacterController>();
		textArea = GameManager.Instance.InputTextArea;
		interactionAction = playerInput.actions["Interaction"];

		InputTextController(false);
	}

	private void Update()
	{
		if(interactionAction.WasPerformedThisFrame())
		{
			Interact();
		}
		if(GameManager.Instance.isInteracting) 
		{
			InputTextController(true);
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			InputTextController(false);
		}
	}

	private void Interact()
	{
		RaycastHit hit;
		Ray ray = new Ray(interactionPoint.position, interactionPoint.forward);

		if (Physics.Raycast(ray, out hit, detectionDistance))
		{
			if (hit.collider.CompareTag("NPC"))
			{
				GameManager.Instance.isInteracting = true;
				interactingNPC = hit.collider.gameObject;
				TurnNPCToPlayer();
				Cursor.lockState = CursorLockMode.Confined;
			}
			else
			{
				GameManager.Instance.isInteracting = false;
			}
		}
	}

	private void TurnNPCToPlayer()
	{
		npcInteractedScript = interactingNPC.GetComponent<NPCInteractedScript>();
		npcInteractedScript.TurnToPlayer(gameObject.transform);
	}

	private void InputTextController(bool activeStatus)
	{
		textArea.SetActive(activeStatus);
		playerCharController.enabled = !activeStatus;
	}
}
