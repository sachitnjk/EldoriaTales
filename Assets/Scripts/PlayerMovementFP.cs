using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementFP : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction moveAction;

	private Animator playerAnimator;
	private CharacterController playerCharController;

	private Vector2 moveInput;
	private Vector3 moveDirection;

	private bool isMoving;
	private bool isGrounded;

	private Vector3 verticalVelocity;

	[SerializeField] private float moveSpeed;

	private void Start()
	{
		playerInput = InputProvider.GetPlayerInput();

		if(playerInput != null ) 
		{
			moveAction = playerInput.actions["Move"];
		}

		playerCharController = GetComponent<CharacterController>();
		playerAnimator = GetComponentInChildren<Animator>();

		isMoving = false;
	}

	private void Update()
	{
		Move();
	}

	private void Move()
	{

		moveInput = moveAction.ReadValue<Vector2>();
		
		moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;

		verticalVelocity.y += Physics.gravity.y * Time.deltaTime;
		playerCharController.Move(verticalVelocity * Time.deltaTime);

		if(playerCharController.isGrounded ) 
		{
			verticalVelocity.y = -2f;
		}


		if(moveDirection.magnitude > 0) 
		{
			playerCharController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
			isMoving = true;
			playerAnimator.SetBool("isWalking", true);
		}
		else
		{
			isMoving = false;
			playerAnimator.SetBool("isWalking", false);
		}
	}
}
