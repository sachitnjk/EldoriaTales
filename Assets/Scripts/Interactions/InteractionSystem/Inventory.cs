using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public static Inventory Instance;

	public List<ColorKD> collectedKeys = new List<ColorKD>();
	public List<ColorKD> doors = new List<ColorKD>();

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(Instance);
		}
	}

	public void OpenDoorTry(ColorKD doorColor)
	{
		if (collectedKeys.Contains(doorColor))
		{
			GameManager.Instance.TriggerOnDoorOpenedEvent(doorColor);
		}
	}

	public void CollectKey(ColorKD key)
	{
		collectedKeys.Add(key);
	}

	public void UseKey(ColorKD keyColor) 
	{
		if(collectedKeys.Contains(keyColor))
		{
			collectedKeys.Remove(keyColor);
			UIManager.Instance.interactionBox.text = keyColor + "Key used";
		}

		StartCoroutine(ResetInteractionBoxAfterDelay(1f));
	}

	private IEnumerator ResetInteractionBoxAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		UIManager.Instance.interactionBox.text = null;
	}

	public bool PlayerHasKey(ColorKD heldKeyColor)
	{
		return collectedKeys.Contains(heldKeyColor);
	}

}
