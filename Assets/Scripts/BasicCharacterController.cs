using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicCharacterController : MonoBehaviour
{
	public float speed = 5;
	public Collider2D characterCollider;
	public CarryController carryController;

	private InputMap controls;
	private Vector2 _movement;
	private Collectible _touchedCollectable = null;

	void Awake()
	{
		controls = new InputMap();
	}

	private void Update()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, characterCollider.bounds.max.y);
	}

	void FixedUpdate()
	{
		transform.position = transform.position + new Vector3(_movement.x, _movement.y) * speed * Time.deltaTime;
	}

	private void OnEnable()
	{
		controls.Player.Move.performed += OnMove;
		controls.Player.Move.canceled += OnMoveEnd;
		controls.Player.Fire.performed += OnFire;
		controls.Enable();
	}

	private void OnDisable()
	{
		controls.Disable();
		controls.Player.Move.performed -= OnMove;
		controls.Player.Move.canceled -= OnMoveEnd;
	}

	void OnMove(InputAction.CallbackContext context)
	{
		_movement = context.ReadValue<Vector2>();
		carryController.UpdateForMove(_movement);
	}

	void OnMoveEnd(InputAction.CallbackContext context)
	{
		_movement = Vector2.zero;
	}

	void OnFire(InputAction.CallbackContext context)
	{
		if (_touchedCollectable != null)
		{
			carryController.PickUp(_touchedCollectable);
			_touchedCollectable = null;
		}
		else
		{
			carryController.Drop();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// TODO: Maybe change this to a separate collider trigger so we can pick up nearby things, not just things we've smashed our face into?
		// Maybe a circle collider?
		var collectable = collision.gameObject.GetComponent<Collectible>();
		if (collectable != null)
			_touchedCollectable = collectable;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		_touchedCollectable = null;
	}
}
