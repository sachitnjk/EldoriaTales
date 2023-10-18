using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCStateBase : MonoBehaviour
{
	protected NPCController npcAI;
	public NPCStateBase(NPCController npcAI)
	{
		this.npcAI = npcAI;
	}

	//Called when npc enters the state
	public abstract void EnterState();
	//Called every frame to update the state
	public abstract void UpdateState();
	//Called when npc exits the state
	public abstract void ExitState();

	public abstract NPCState GetStateEnum();
}
