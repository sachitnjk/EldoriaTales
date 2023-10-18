using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : NPCStateBase
{
	public IdleState(NPCController npcAI) : base(npcAI)
	{
	}

	public override void EnterState()
	{
	}
	public override void UpdateState()
	{
	}
	public override void ExitState()
	{
	}

	public override NPCState GetStateEnum()
	{
		return NPCState.Idle;
	}
}
