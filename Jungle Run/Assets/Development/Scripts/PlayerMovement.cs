using UnityEngine;
using UnityEngine.InputSystem;
using UnityAtoms.BaseAtoms; //Add this to use UnityAtoms inside script

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpForce = 15.0f;

    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;

    [SerializeField] private VoidEvent onJump; // Jumping event

    private Rigidbody2D _rigidbody;
    private float _playersMovementDirection = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // enable and subscribe actions so the player will respond on input
        moveAction.action.Enable();
        jumpAction.action.Enable();

        moveAction.action.performed += OnMove;
        moveAction.action.canceled += OnMoveCanceled;
        jumpAction.action.performed += OnJump;
    }

    private void OnDisable()
    {
        // unsubscribe and disable actions OnDisable to prevent bugs
        moveAction.action.performed -= OnMove;
        moveAction.action.canceled -= OnMoveCanceled;
        jumpAction.action.performed -= OnJump;

        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _playersMovementDirection = context.ReadValue<float>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _playersMovementDirection = 0; // Reset movement direction when input is canceled
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        // Handle jumping
        if (Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            onJump.Raise(); // Raise jumping event
            _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        // Apply horizontal movement
        _rigidbody.velocity = new Vector2(_playersMovementDirection * speed, _rigidbody.velocity.y);
    }
}
