using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChattingPlayerState : NPCStateBase
{
	public ChattingPlayerState(NPCController npcAI) : base(npcAI)
	{
	}

	public override void EnterState()
	{
		npcAI.navMeshAgent.destination = npcAI.transform.position;
		//npcAI.SetCurrentSpeed(0f);
	}
	public override void UpdateState()
	{
		if(!GameManager.Instance.isInteractingWithPlayer)
		{
			npcAI.ChangeState(NPCState.Idle);
		}
	}
	public override void ExitState()
	{
		npcAI.navMeshAgent.ResetPath();
	}

	public override NPCState GetStateEnum()
	{
		return NPCState.ChattingPlayer;
	}
}
