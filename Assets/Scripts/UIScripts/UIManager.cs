using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public ChatGPTManager chatGPTManager;
	public TextMeshProUGUI chatInputField;

	private PlayerInput playerInput;
	private InputAction enterAction;

	private string userInput;
	private string npcName;

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
