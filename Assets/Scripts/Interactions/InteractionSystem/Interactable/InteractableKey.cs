using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableKey : BaseInteractable
{
	[SerializeField] private ColorKD keyColor;
	protected void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);
		if (base.playerInRange)
		{
			InteractableTriggerAction();
		}
	}

	protected override void InteractableTriggerAction()
	{
		//Debug.Log("interact to collect key");
	}

	protected override void OnInteract()
	{
		base.OnInteract();
		Inventory.Instance.CollectKey(keyColor);
		this.gameObject.SetActive(false);
	}
}
