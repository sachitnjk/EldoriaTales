using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using System;
using System.IO;
using static UnityEngine.EventSystems.EventTrigger;
using Newtonsoft.Json;

public class NPCData
{
	public string backstory;
	public List<ChatMessage> conversationHistory;

	public NPCData()
		{
			conversationHistory = new List<ChatMessage>();
		}
}

public class ChatGPTManager : MonoBehaviour
{
	private OpenAIApi openAI = new OpenAIApi();

	private Dictionary<string, NPCData> npcDataDictionary = new Dictionary<string, NPCData>();

	// NPC-specific data
	private void InitializeNPCData()
	{
		npcDataDictionary.Add("Jane", new NPCData {
			backstory = "You are a farmer's daughter. Your earliest memory of this place is, waking up here one day and finding yourself to be a bean like character. You are confused but happy that you dont have much of anything to worry about. Play this role for any questions or comments directed towards you. Do not break character. Even if the same question is being asked multiple times."
		});
		npcDataDictionary.Add("Luna", new NPCData { 
			backstory = "You are from the year 2050, while exploring a cave, you fell through a hole and ended up in this mysterious place where everyone is bean shaped. Play this role for any questions or comments directed towards you. Do not break character. Even if the same question is being asked multiple times."
		});
		npcDataDictionary.Add("Blake", new NPCData
		{
			backstory = "You were a cowboy who was heardig his cattle. Suddenly you saw a bright light in the sky and you found yourself to be in this place with two other people Jane, who was a farmers daughter and Luna who claims she is from the future but you think she is crazy. Play this role for any questions or comments directed towards you. DO not break character. Even if the same question is being asked multiple times."
		});
		npcDataDictionary.Add("Elise", new NPCData
		{
			backstory = "You are a time traveller and the only one out of Jane, Luna and Blake who knows what the place you are in really is. You know that this is a game and everyone is a character including you. You dont know how to feel about it. Play this role for any questions or comments directed towards you. Do not break character. Even if the same question is being asked multiple times."
		});
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
			//Including NPC's name in the conversation
			ChatMessage nameMessage = new ChatMessage { Content = $"My name is {npcName}", Role = "system" };
			npcData.conversationHistory.Add(nameMessage);

			// Including NPC's backstory in the conversation
			ChatMessage backstoryMessage = new ChatMessage { Content = npcData.backstory, Role = "system" };
			npcData.conversationHistory.Add(backstoryMessage);
		}

		// User message
		ChatMessage userMessageObj = new ChatMessage { Content = userMessage, Role = "user" };
		npcData.conversationHistory.Add(userMessageObj);

		// Query ChatGPT
		CreateChatCompletionRequest request = new CreateChatCompletionRequest();
		request.Messages = npcData.conversationHistory;
		request.Model = "gpt-3.5-turbo";

		var response = await openAI.CreateChatCompletion(request);

		if (response.Choices != null && response.Choices.Count > 0)
		{
			var chatResponse = response.Choices[0].Message;
			npcData.conversationHistory.Add(chatResponse);

			GameManager.Instance.chatOutputField.text += "\n" + chatResponse.Content;
			//Debug.Log(chatResponse.Content);
		}

		// Clear messages if needed, keeping commented for now
		//messages.Clear();
	}
}
