using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : NPCStateBase
{
	private float minIdleDuration = 2f;
	private float maxIdleDuration = 6f;
	private float currentIdleDuration = 0f;
	private float idleTimer;
	public IdleState(NPCController npcAI) : base(npcAI)
	{
	}
	public override void EnterState()
	{
		SetRandomIdleTime();
		idleTimer = 0f;
		Debug.Log("going here");
	}
	public override void UpdateState()
	{
		idleTimer += Time.deltaTime;

		if (idleTimer >= currentIdleDuration)
		{
			Debug.Log("Changing to move");
			//npcAI.ChangeState(NPCState.IdleMove);
		}
	}
	public override void ExitState()
	{
	}

	public override NPCState GetStateEnum()
	{
		return NPCState.Idle;
	}

	private void SetRandomIdleTime()
	{
		currentIdleDuration = Random.Range(minIdleDuration, maxIdleDuration);
	}
}
