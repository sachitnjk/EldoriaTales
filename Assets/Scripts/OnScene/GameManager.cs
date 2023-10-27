using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	[field: SerializeField] public GameObject InputTextArea { get;private set; }
	public bool isInteractingWithPlayer {  get; set; }
	public bool isInteractingWithNPC {  get; set; }

	public event Action<ColorKD> OnDoorOpened;

	public TextMeshProUGUI chatOutputField;

	public string interactingNPCName { get; set; }

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

	public void TriggerOnDoorOpenedEvent(ColorKD doorColor)
	{
		OnDoorOpened?.Invoke(doorColor);
	}

	private void Start()
	{
		isInteractingWithPlayer = false;
	}
}
