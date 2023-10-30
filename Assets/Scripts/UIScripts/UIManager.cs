using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance { get; private set; }

	public ChatGPTManager chatGPTManager;
	public TextMeshProUGUI chatInputField;
	public TextMeshProUGUI interactionBox;
	public TextMeshProUGUI doorInteractionBox;

	private PlayerInput playerInput;
	private InputAction enterAction;

	private string userInput;
	private string npcName;

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
		playerInput = InputProvider.GetPlayerInput();
		enterAction = playerInput.actions["Enter"];
	}

	private void Update()
	{
		if(GameManager.Instance.isInteractingWithPlayer && enterAction.WasPerformedThisFrame())
		{
			userInput = chatInputField.text;
			npcName = GameManager.Instance.interactingNPCName;
			chatGPTManager.AskChatGPT(npcName, userInput);
			chatInputField.text = "";
		}
	}

	public void ReturnFromConversation()
	{
		GameManager.Instance.isInteractingWithPlayer = false;
	}

}
