using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleMoveState : NPCStateBase
{
	private float minIdleMoveDuration = 2f;
	private float maxIdleMoveDuration = 6f;
	private float currentIdleMoveDuration = 0f;
	private float idleMoveTimer;

	private float currentSpeed;
	private float rotationSpeed = 10f;
	private float distanceMoved = 0f;
	private float distanceUntilChangeDirection = 10f;

	private bool isMovingRandomly;
	private Vector3 randomDirection;
	public IdleMoveState(NPCController npcAI) : base(npcAI)
	{
	}

	public override void EnterState()
	{
		currentSpeed = npcAI.CurrentSpeed;
		SetRandomIdleMoveTime();
		idleMoveTimer = 0f;
	}
	public override void UpdateState()
	{
		idleMoveTimer += Time.deltaTime;

		MoveRandomly();
		if (idleMoveTimer >= currentIdleMoveDuration)
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
		return NPCState.IdleMove;
	}

	private void SetRandomIdleMoveTime()
	{
		currentIdleMoveDuration = Random.Range(minIdleMoveDuration, maxIdleMoveDuration);
	}

	private void MoveRandomly()
	{
		if (!isMovingRandomly)
		{
			isMovingRandomly = true;
			randomDirection = Random.insideUnitSphere;
			distanceMoved = 0f;
		}
		float movementDistance = currentSpeed * Time.deltaTime;
		Vector3 movementVector = randomDirection * movementDistance;
		if (npcAI.navMeshAgent != null && npcAI.navMeshAgent.enabled)
		{
			if (!EdgeDetection(movementVector))
			{
				npcAI.navMeshAgent.Move(movementVector);
			}
			else
			{
				randomDirection = -randomDirection;
			}
		}
		else
		{
			npcAI.transform.position += movementVector;
		}

		distanceMoved += movementDistance;

		if (distanceMoved >= distanceUntilChangeDirection)
		{
			isMovingRandomly = false;
		}
		RotateEntity();
	}

	//Function to rotate the entity towards the current forward
	private void RotateEntity()
	{
		Vector3 movementDirection = new Vector3(randomDirection.x, 0f, randomDirection.z).normalized;
		if (movementDirection != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
			npcAI.transform.rotation = Quaternion.Lerp(npcAI.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}
	}

	private bool EdgeDetection(Vector3 movementVector)
	{
		NavMeshHit hit;
		if (NavMesh.Raycast(npcAI.transform.position, npcAI.transform.position + movementVector, out hit, NavMesh.AllAreas))
		{
			return true;
		}
		return false;
	}
}
