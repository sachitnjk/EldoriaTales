using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	[field: SerializeField] public GameObject InputTextArea { get;private set; }
	public bool isInteracting {  get; set; }

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

	private void Start()
	{
		isInteracting = false;
	}
}
