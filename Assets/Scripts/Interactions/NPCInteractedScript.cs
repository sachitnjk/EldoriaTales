using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class NPCInteractedScript : MonoBehaviour
{
	private Vector3 directionToPlayer;

	public void TurnToPlayer(Transform player)
	{
		directionToPlayer = player.position - transform.position;
		directionToPlayer.y = 0f;

		transform.rotation = Quaternion.LookRotation(directionToPlayer);
	}
}
