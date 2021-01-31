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

	[SerializeField]
	private TaskUIController uiController;

    //hidden variables
    private InputMap input;
    private Vector2 moveVector;

	//events
	public UnityEvent onMovePress;
    public UnityEvent onMoveRelease;

    //messages
    private void Awake()
	{
        input = new InputMap();
        rb2D = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

	public void Disable()
	{
		this.enabled = false;
		animator.SetFloat("Speed", 0f);
	}

    private void OnMovePress( InputAction.CallbackContext ctx )
	{
        Vector2 vex = ctx.ReadValue<Vector2>();
		carryController.UpdateForMove(vex);
		animator.SetFloat( "Horizontal", vex.x );
        animator.SetFloat( "Vertical", vex.y );
        animator.SetFloat( "Speed", vex.magnitude );
        if (moveVector.magnitude == 0 && vex.magnitude != 0)
        {
			onMovePress?.Invoke();
        }
        moveVector = ctx.ReadValue<Vector2>();

    }

    private void OnMoveRelease( InputAction.CallbackContext ctx )
	{
        moveVector = Vector2.zero;
        animator.SetFloat( "Speed", moveVector.magnitude );
        onMoveRelease?.Invoke();
    }

	private void OnInteract(InputAction.CallbackContext context)
	{
		carryController.HandleInteractPressed();
	}

	private void OnMapPress(InputAction.CallbackContext context)
	{
		uiController.ShowUI();
	}

	private void OnMapRelease(InputAction.CallbackContext context)
	{
		uiController.HideUI();
	}

	private void OnEnable()
	{
        input.Player.Move.performed += OnMovePress;
        input.Player.Move.canceled += OnMoveRelease;
		input.Player.Interact.performed += OnInteract;
		input.Player.TaskList.performed += OnMapPress;
		input.Player.TaskList.canceled += OnMapRelease;
		input.Enable();
    }

    private void OnDisable()
	{
		input.Player.Move.performed -= OnMovePress;
        input.Player.Move.canceled -= OnMoveRelease;
		input.Player.Interact.performed -= OnInteract;
		input.Player.TaskList.performed -= OnMapPress;
		input.Player.TaskList.canceled -= OnMapRelease;
		input.Disable();
	}

    private void FixedUpdate()
	{
        //force movement
        rb2D.AddForce( moveVector * moveSpeed * Time.fixedDeltaTime * 5000f, ForceMode2D.Force );
        
        //static movement
        //rb2D.MovePosition( rb2D.position + moveVector * moveSpeed * Time.fixedDeltaTime );
    }

	private void Update()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, characterCollider.bounds.max.y);
	}
}
