using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicCharacterController : MonoBehaviour
{
	public float speed = 5;

	private InputMap controls;
	private Vector2 _movement = Vector2.zero;

	void Awake()
	{
		controls = new InputMap();

	}

	private void OnEnable()
	{
		controls.Enable();
		controls.Player.Move.started += OnMoveStart;
		controls.Player.Move.canceled += OnMoveEnd;
	}

	private void OnDisable()
	{
		controls.Disable();
		controls.Player.Move.started -= OnMoveStart;
		controls.Player.Move.canceled -= OnMoveEnd;
	}

	//void FixedUpdate()
	//{
	//	//transform.position = transform.position + new Vector3(_movement.x, _movement.y) * speed * Time.deltaTime;
	//}


	void OnMoveStart(InputAction.CallbackContext context)
	{
		_movement = context.ReadValue<Vector2>();
		Debug.Log("MoveStart " + _movement);
	}

	void OnMoveEnd(InputAction.CallbackContext context)
	{
		_movement = Vector2.zero;
		Debug.Log("MoveEnd " + _movement);
	}

	//void OnMove(InputAction.CallbackContext context)
	//{
	//	var input = context.ReadValue<Vector2>().normalized;
	//	Debug.Log("YEP! " + input);

	//	//_movement = context.ReadValue<Vector2>();
	//}
}
