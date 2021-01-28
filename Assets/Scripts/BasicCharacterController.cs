using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicCharacterController : MonoBehaviour
{
	public float speed = 5;
	public Collider2D col2d;

	private InputMap controls;
	private Vector2 _movement;
	private Collectable _touchedCollectable = null;

	void Awake()
	{
		controls = new InputMap();
	}

	private void Update()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, col2d.bounds.max.y);
	}

	void FixedUpdate()
	{
		transform.position = transform.position + new Vector3(_movement.x, _movement.y) * speed * Time.deltaTime;
	}

	private void OnEnable()
	{
		controls.Player.Move.performed += OnMoveStart;
		controls.Player.Move.canceled += OnMoveEnd;
		controls.Player.Fire.performed += OnFire;
		controls.Enable();
	}

	private void OnDisable()
	{
		controls.Disable();
		controls.Player.Move.performed -= OnMoveStart;
		controls.Player.Move.canceled -= OnMoveEnd;
	}

	void OnMoveStart(InputAction.CallbackContext context)
	{
		_movement = context.ReadValue<Vector2>();
	}

	void OnMoveEnd(InputAction.CallbackContext context)
	{
		_movement = Vector2.zero;
	}

	void OnFire(InputAction.CallbackContext context)
	{
		if (_touchedCollectable != null)
		{
			_touchedCollectable.col2d.isTrigger = true;
			_touchedCollectable.gameObject.transform.SetParent(this.gameObject.transform);
			
			// TODO: Need to move this transform to be relative to the hands object?
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// TODO: Maybe change this to a separate collider trigger so we can pick up nearby things, not just things we've smashed our face into?
		// Maybe a circle collider?
		var collectable = collision.gameObject.GetComponent<Collectable>();
		if (collectable != null)
			_touchedCollectable = collectable;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		_touchedCollectable = null;
	}
}
