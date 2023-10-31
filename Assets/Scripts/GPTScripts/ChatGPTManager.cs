using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using System;
using System.IO;
using static UnityEngine.EventSystems.EventTrigger;
using Newtonsoft.Json;

[System.Serializable]
public class NPCDataEntry
{
	public string npcName;

	[TextArea(10, 15)]
	public string backstory;
}

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

	[SerializeField] private string questToPlayer;

	[SerializeField]
	private List<NPCDataEntry> npcDataEntries;

	private Dictionary<string, NPCData> npcDataDictionary = new Dictionary<string, NPCData>();

	// NPC-specific data
	private void InitializeNPCData()
	{
		foreach(var entry in npcDataEntries)
		{
			string otherNPCInfo = "";
			foreach(var otherEntry in npcDataEntries)
			{
				//excluding current npc from list of other NPCs
				if(entry != otherEntry) 
				{
					//passing the name and backstory of other npc also added delimiter "|" to seperate stories
					otherNPCInfo += $"{otherEntry.npcName}: {otherEntry.backstory}|";
				}
			}
			npcDataDictionary.Add(entry.npcName, new NPCData
			{
				backstory = entry.backstory + otherNPCInfo + questToPlayer + "Play this role for any questions or comments directed towards you. Do not break character. Even if the same question is being asked multiple times."
			});
		}
	}

	private void Awake()
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
		}
	}
}
