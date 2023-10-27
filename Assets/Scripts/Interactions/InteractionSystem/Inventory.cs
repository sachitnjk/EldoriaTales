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
		if(collectedKeys.Contains(doorColor))
		{
			GameManager.Instance.TriggerOnDoorOpenedEvent(doorColor);
			//Debug.Log("Door opened");
		}
		else
		{
			Debug.Log("Key for door missing");
		}
	}

	public void CollectKey(ColorKD key)
	{
		collectedKeys.Add(key);
		Debug.Log(key + "key collected");
	}

	public void UseKey(ColorKD keyColor) 
	{
		if(collectedKeys.Contains(keyColor))
		{
			collectedKeys.Remove(keyColor);
			Debug.Log(keyColor + "key used");
		}
		else
		{
			Debug.Log("key not found in inventory");
		}
	}

	public bool PlayerHasKey(ColorKD heldKeyColor)
	{
		return collectedKeys.Contains(heldKeyColor);
	}
}
