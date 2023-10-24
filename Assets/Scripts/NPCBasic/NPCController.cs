using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class NPCController : MonoBehaviour
{
	public NPCState initialState;
	public float CurrentSpeed {  get;private set; }

	[Header("NPC basic attributes")]
	[SerializeField] private float detectionRadius;
	[field: SerializeField] public float RotationSpeed { get; private set; }

	[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public NPCInteractedScript npcInteractionScript;

	private NPCStateBase currentState;

	private void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		npcInteractionScript = GetComponent<NPCInteractedScript>();

		CurrentSpeed = 4f;

		currentState = CreateState(initialState);
		currentState.EnterState();
	}

	private void Update()
	{
		if (currentState != null)
		{
			currentState.UpdateState();
		}

		InteractionCheckChange();
	}

	private NPCStateBase CreateState(NPCState state)
	{

		switch (state)
		{
			case NPCState.Idle:
				return new IdleState(this);
			case NPCState.IdleMove:
				return new IdleMoveState(this);
			case NPCState.ChattingPlayer:
				return new ChattingPlayerState(this);
			default:
				return null;
		}
	}

	public void ChangeState(NPCState newState)
	{
		if (currentState != null)
		{
			currentState.ExitState();
		}
		currentState = CreateState(newState);

		if (currentState != null)
		{
			currentState.EnterState();
		}
	}

	private void InteractionCheckChange()
	{
		if (GameManager.Instance.isInteractingWithPlayer && IsInteractedNPCCheck())
		{
			ChangeState(NPCState.ChattingPlayer);
		}
		if (!GameManager.Instance.isInteractingWithPlayer && GameManager.Instance.isInteractingWithNPC)
		{
			ChangeState(NPCState.ChattingNPC);
		}
	}

	private bool IsInteractedNPCCheck()
	{
		if(npcInteractionScript.npcName == GameManager.Instance.interactingNPCName)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void SetCurrentSpeed(float updatedSpeed)
	{
		CurrentSpeed = updatedSpeed;
	}

}
