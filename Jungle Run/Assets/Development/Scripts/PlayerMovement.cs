using UnityEngine;
using UnityEngine.InputSystem;
using UnityAtoms.BaseAtoms;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float startSpeed = 5.0f; // Initial movement speed
    [SerializeField] private float speedIncrease = 0.5f; // Speed increment over time
    [SerializeField] private float speedIncreaseInterval = 10.0f; // Time interval for speed increase
    [SerializeField] private float speedToActivateRunMode = 5.0f; // Speed threshold to activate running mode
    [SerializeField] private float initialJumpForce = 10.0f; // Force for initial jump
    [SerializeField] private float additionalJumpForce = 40.0f; // Force while holding the jump button
    [SerializeField] private float maxJumpTime = 0.35f; // Max time player can hold jump to rise
    [SerializeField] private float fallGravityMultiplier = 2.0f; // Increased gravity when falling

    [SerializeField] private InputActionReference jumpAction; // Input action for jumping

    [SerializeField] private VoidEvent onJump; // Jumping event from UnityAtoms
    [SerializeField] private VoidEvent onRun; // Running event from UnityAtoms
    [SerializeField] private VoidEvent onWalk; // Walking event from UnityAtoms

    private float _speed; // Current movement speed
    private bool _isRunning = false; // Is the player running
    private Rigidbody2D _rigidbody; // Reference to the Rigidbody2D component
    private Coroutine _increaseSpeedRoutine; // Coroutine for gradually increasing speed
    private bool _isJumping; // Is the player currently jumping
    private float _jumpTimeCounter; // Timer to limit jump duration
    private float _gravityScale; // Original gravity scale

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component
        _gravityScale = _rigidbody.gravityScale; // Store the original gravity scale
    }

    private void Start()
    {
        _speed = startSpeed; // Initialize speed
    }

    private void OnEnable()
    {
        // Enable input action
        jumpAction.action.Enable();

        // Subscribe to input events
        jumpAction.action.started += OnJumpStarted;
        jumpAction.action.canceled += OnJumpCanceled;

        // Start speed increase coroutine
        _increaseSpeedRoutine = StartCoroutine(IncreaseSpeedRoutine());
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        jumpAction.action.started -= OnJumpStarted;
        jumpAction.action.canceled -= OnJumpCanceled;

        // Disable input action
        jumpAction.action.Disable();

        if (_increaseSpeedRoutine != null)
            StopCoroutine(_increaseSpeedRoutine); // Stop speed increase routine
    }

    private IEnumerator IncreaseSpeedRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedIncreaseInterval); // Wait for the interval
            _speed += speedIncrease; // Increase speed

            if (_speed >= speedToActivateRunMode && !_isRunning)
            {
                _isRunning = true; // Set running mode to true
                onRun.Raise(); // Trigger run event
            }
        }
    }

    public void ActivateWalkDebuff()
    {
        // Halve the player's speed or set it to startSpeed if it's higher
        _speed = Mathf.Max(startSpeed, _speed / 2);

        if (_increaseSpeedRoutine != null)
        {
            StopCoroutine(_increaseSpeedRoutine); // Stop the routine if active
            _increaseSpeedRoutine = StartCoroutine(IncreaseSpeedRoutine()); // Restart routine
        }

        // Switch back to walking mode if the speed drops below running threshold
        if (_isRunning && _speed < speedToActivateRunMode)
        {
            _isRunning = false; // Set running mode to false
            onWalk.Raise(); // Trigger walk event
        }
    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        if (Mathf.Abs(_rigidbody.velocity.y) < 0.001f && !_isJumping) // Check if grounded and not already jumping
        {
            _isJumping = true; // Set jumping flag
            _jumpTimeCounter = maxJumpTime; // Reset jump time counter
            _rigidbody.AddForce(new Vector2(0, initialJumpForce), ForceMode2D.Impulse); // Apply initial jump force
            onJump.Raise(); // Trigger jump event
        }
    }

    public void OnJumpStarted()
    {
        if (Mathf.Abs(_rigidbody.velocity.y) < 0.001f && !_isJumping) // Check if grounded and not already jumping
        {
            _isJumping = true; // Set jumping flag
            _jumpTimeCounter = maxJumpTime; // Reset jump time counter
            _rigidbody.AddForce(new Vector2(0, initialJumpForce), ForceMode2D.Impulse); // Apply initial jump force
            onJump.Raise(); // Trigger jump event
        }
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        if (_isJumping)
        {
            _isJumping = false; // Stop jump when input is released
        }
    }

    public void OnJumpCanceled()
    {
        if (_isJumping)
        {
            _isJumping = false; // Stop jump when input is released
        }
    }

    private void FixedUpdate()
    {
        // Apply horizontal movement
        _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);

        // Handle extended jump if the button is held
        if (_isJumping && _jumpTimeCounter > 0)
        {
            _rigidbody.AddForce(new Vector2(0, additionalJumpForce), ForceMode2D.Force); // Apply extra jump force
            _jumpTimeCounter -= Time.fixedDeltaTime; // Decrease jump timer
        }
        else
        {
            _isJumping = false; // End jumping
        }

        // Apply increased gravity when falling
        _rigidbody.gravityScale = _rigidbody.velocity.y < 0 ? _gravityScale * fallGravityMultiplier : _gravityScale;
    }
}