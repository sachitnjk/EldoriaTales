using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction interactionAction;

	private void Start()
	{
		playerInput = InputProvider.GetPlayerInput();
		interactionAction = playerInput.actions["Interaction"];
	}
	private void OnTriggerEnter(Collider other)
	{
		SceneManager.LoadScene(2);
	}
}
