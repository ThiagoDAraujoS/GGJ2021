using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //components
    private Rigidbody2D rb2D = null;
    private Animator animator = null;
    
    //input variables
    [SerializeField][Range(0.0f, 15.0f)]
    private float moveSpeed;

    //hidden variables
    private InputMap input;
    private Vector2 moveVector;

    //events
    private UnityEvent onMovePress;
    private UnityEvent onMoveRelease;

    //messages
    private void Awake() {
        input = new InputMap();
        rb2D = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnMovePress( InputAction.CallbackContext ctx ) {
        moveVector = ctx.ReadValue<Vector2>();
        animator.SetFloat( "Horizontal", moveVector.x );
        animator.SetFloat( "Vertical", moveVector.y );
        animator.SetFloat( "Speed", moveVector.magnitude );
        onMovePress?.Invoke();
        Debug.Log( "Pressed " + moveVector );
    }

    private void OnMoveRelease( InputAction.CallbackContext ctx ) {
        moveVector = Vector2.zero;
        animator.SetFloat( "Speed", moveVector.magnitude );
        onMoveRelease?.Invoke();
        Debug.Log( "Stopped" );
    }

    private void OnEnable() {
        input.Player.Move.performed += OnMovePress;
        input.Player.Move.canceled += OnMoveRelease;
        input.Enable();
    }

    private void OnDisable() {
        input.Player.Move.performed -= OnMovePress;
        input.Player.Move.canceled -= OnMoveRelease;
        input.Disable();
    }

    private void Update() {
        
    }

    private void FixedUpdate() {
        rb2D.MovePosition( rb2D.position + moveVector * moveSpeed * Time.fixedDeltaTime );
    }
}
