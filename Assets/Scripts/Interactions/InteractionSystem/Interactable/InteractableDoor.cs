using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableDoor : BaseInteractable
{
	[SerializeField] private ColorKD associatedDoorColor;
	private Animator doorAnimator;
	private Collider doorTriggerCollider;

	protected override void Start()
	{
		base.Start();

		doorAnimator = GetComponentInChildren<Animator>();
		doorTriggerCollider = GetComponent<Collider>();

		GameManager.Instance.OnDoorOpened += OpenDoor;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		GameManager.Instance.OnDoorOpened -= OpenDoor;
	}

	protected new void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);
		if(base.playerInRange)
		{
			InteractableTriggerAction();
		}
	}

	protected override void OnInteract()
	{
		base.OnInteract();
		Inventory.Instance.OpenDoorTry(associatedDoorColor);
		Inventory.Instance.UseKey(associatedDoorColor);
	}

	protected override void InteractableTriggerAction()
	{
		if (Inventory.Instance.PlayerHasKey(associatedDoorColor))
		{
			Debug.Log("Interact");
		}
		else if (!Inventory.Instance.PlayerHasKey(associatedDoorColor))
		{
			Debug.Log("get correct key to interact");
		}
	}

	private void OpenDoor(ColorKD doorColor)
	{
		if(doorColor == associatedDoorColor) 
		{
			doorTriggerCollider.enabled = false;
			doorAnimator?.SetTrigger("OpenDoor");
		}
	}
}
