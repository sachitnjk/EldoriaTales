using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using System;
using System.IO;
using static UnityEngine.EventSystems.EventTrigger;
using Newtonsoft.Json;

public class ChatGPTManager : MonoBehaviour
{
	private OpenAIApi openAI = new OpenAIApi();
	private List<ChatMessage> messages = new List<ChatMessage>();

	private Dictionary<string, NPCData> npcDataDictionary = new Dictionary<string, NPCData>();

	// NPC-specific data (example data, you should populate this dictionary with your NPC data)
	private void InitializeNPCData()
	{
		npcDataDictionary.Add("NPC1", new NPCData { name = "NPC1", backstory = "You are Jane, you were a farmer's daughter. your earliest memory of this place she is in right now, is waking up here one day and finding herself to be a bean like character. This is backstroy for Jane, play the role of Jane for any questions or comments directed towards you. Do not break character" });
		npcDataDictionary.Add("NPC2", new NPCData { name = "NPC2", backstory = "Backstory for NPC2..." });
		// Add data for other NPCs
	}

	private void Start()
	{
		InitializeNPCData();
	}

	public async void AskChatGPT(string npcName, string userMessage)
	{
		// Retrieve NPC-specific data
		NPCData npcData;
		if (npcDataDictionary.TryGetValue(npcName, out npcData))
		{
			// Include NPC's backstory in the conversation
			ChatMessage backstoryMessage = new ChatMessage { Content = npcData.backstory, Role = "system" };
			messages.Add(backstoryMessage);
		}

		// User message
		ChatMessage userMessageObj = new ChatMessage { Content = userMessage, Role = "user" };
		messages.Add(userMessageObj);

		// Query ChatGPT
		CreateChatCompletionRequest request = new CreateChatCompletionRequest();
		request.Messages = messages;
		request.Model = "gpt-3.5-turbo";

		var response = await openAI.CreateChatCompletion(request);

		if (response.Choices != null && response.Choices.Count > 0)
		{
			var chatResponse = response.Choices[0].Message;
			messages.Add(chatResponse);

			Debug.Log(chatResponse.Content);
		}

		// Clear messages for the next conversation
		messages.Clear();
	}
}
