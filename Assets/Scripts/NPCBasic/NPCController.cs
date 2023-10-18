using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

public class NPCController : MonoBehaviour
{
	public NPCState initialState;
	public float CurrentSpeed {  get;private set; }

	[Header("NPC basic attributes")]
	[SerializeField] private float movementSpeed;
	[SerializeField] private float detectionRadius;
	[field: SerializeField] public float RotationSpeed { get; private set; }

	private NPCStateBase currentState;

	private void Start()
	{
		currentState = CreateState(initialState);
		currentState.EnterState();
	}

	private void Update()
	{
		if (currentState != null)
		{
			currentState.UpdateState();
		}
		Debug.Log(currentState);
	}

	private NPCStateBase CreateState(NPCState state)
	{
		switch (state)
		{
			case NPCState.Idle:
				return new IdleState(this);
			//case NPCState.IdleMove:
			//	return new IdleMoveState(this);
			//case NPCState.ChattingNPC:
			//return new ChattingNPCState(this);
			//case NPCState.ChattingPlayer:
			//return new ChattingPlayer(this);
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

	private void SetMovementSpeed(float updatedSpeed)
	{
		CurrentSpeed = updatedSpeed;
	}

	//private void InteractionCheckChange()
	//{
	//	if(GameManager.Instance.isInteractingWithPlayer) 
	//	{
	//		ChangeState(NPCState.ChattingPlayer);
	//	}
	//	if(!GameManager.Instance.isInteractingWithPlayer && GameManager.Instance.isInteractingWithNPC) 
	//	{
	//		ChangeState(NPCState.ChattingNPC);
	//	}
	//}

}
