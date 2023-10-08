using System.Collections;
using System.Collections.Generic;
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
	private bool isInteracting;

	private void Start()
	{
		playerInput = InputProvider.GetPlayerInput();
		interactionAction = playerInput.actions["Interaction"];

		isInteracting = false;
	}

	private void Update()
	{
		if(interactionAction.WasPerformedThisFrame())
		{
			Interact();
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
				isInteracting = true;
				interactingNPC = hit.collider.gameObject;
				TurnNPCToPlayer();
			}
			else
			{
				isInteracting = false;
			}
		}
	}

	private void TurnNPCToPlayer()
	{
		npcInteractedScript = interactingNPC.GetComponent<NPCInteractedScript>();
		npcInteractedScript.TurnToPlayer(gameObject.transform);
	}
}
