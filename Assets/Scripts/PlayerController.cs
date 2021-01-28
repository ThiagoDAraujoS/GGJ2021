using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputMap input;
    Vector2 move;

    void Awake() {
        input = new InputMap();
    }

    private void OnMovePress( InputAction.CallbackContext ctx ) {
        move = ctx.ReadValue<Vector2>();
        Debug.Log( "Pressed " + move );
    }

    private void OnMoveRelease( InputAction.CallbackContext ctx ) {
        move = Vector2.zero;
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

    // Update is called once per frame
    void Update() {
        
    }
}
