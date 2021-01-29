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

	[SerializeField]
	private Collider2D characterCollider;

	[SerializeField]
	private CarryController carryController;

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
		carryController.UpdateForMove(moveVector);
		animator.SetFloat( "Horizontal", moveVector.x );
        animator.SetFloat( "Vertical", moveVector.y );
        animator.SetFloat( "Speed", moveVector.magnitude );
        onMovePress?.Invoke();
    }

    private void OnMoveRelease( InputAction.CallbackContext ctx ) {
        moveVector = Vector2.zero;
        animator.SetFloat( "Speed", moveVector.magnitude );
        onMoveRelease?.Invoke();
    }

	private void OnFire(InputAction.CallbackContext context) {
		carryController.HandleFirePressed();
	}

	private void OnEnable() {
        input.Player.Move.performed += OnMovePress;
        input.Player.Move.canceled += OnMoveRelease;
		input.Player.Fire.performed += OnFire;
        input.Enable();
    }

    private void OnDisable() {
		input.Player.Move.performed -= OnMovePress;
        input.Player.Move.canceled -= OnMoveRelease;
		input.Player.Fire.performed -= OnFire;
		input.Disable();
	}

    private void FixedUpdate() {
        //force movement
        rb2D.AddForce( moveVector * moveSpeed * Time.fixedDeltaTime * 5000f, ForceMode2D.Force );
        
        //static movement
        //rb2D.MovePosition( rb2D.position + moveVector * moveSpeed * Time.fixedDeltaTime );
    }

	private void Update() {
		transform.position = new Vector3(transform.position.x, transform.position.y, characterCollider.bounds.max.y);
	}
}
