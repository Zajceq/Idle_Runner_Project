using UnityEngine;
using UnityEngine.InputSystem;
using UnityAtoms.BaseAtoms;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float startSpeed = 5.0f;
    [SerializeField] private float speedIncrease = 0.5f;
    [SerializeField] private float speedIncreaseInterval = 10.0f;
    [SerializeField] private float speedToActivateRunMode = 5.0f;
    [SerializeField] private float initialJumpForce = 10.0f;
    [SerializeField] private float additionalJumpForce = 40.0f; // Additional force applied while the button is held
    [SerializeField] private float maxJumpTime = 0.35f; // Maximum time the player can hold the jump button to continue rising
    [SerializeField] private float fallGravityMultiplier = 2.0f; // Multiplier for gravity when falling

    [SerializeField] private InputActionReference jumpAction;

    [SerializeField] private VoidEvent onJump;
    [SerializeField] private VoidEvent onRun;
    [SerializeField] private VoidEvent onWalk;

    private float _speed;
    private bool _isRunning = false;
    private Rigidbody2D _rigidbody;
    private Coroutine _increaseSpeedRoutine;
    private bool _isJumping;
    private float _jumpTimeCounter;
    private float _gravityScale;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _gravityScale = _rigidbody.gravityScale;
    }

    private void Start()
    {
        _speed = startSpeed;
    }

    private void OnEnable()
    {
        jumpAction.action.Enable();
        jumpAction.action.started += OnJumpStarted;
        jumpAction.action.canceled += OnJumpCanceled;

        _increaseSpeedRoutine = StartCoroutine(IncreaseSpeedRoutine());
    }

    private void OnDisable()
    {
        jumpAction.action.started -= OnJumpStarted;
        jumpAction.action.canceled -= OnJumpCanceled;
        jumpAction.action.Disable();

        if (_increaseSpeedRoutine != null)
            StopCoroutine(_increaseSpeedRoutine);
    }

    private IEnumerator IncreaseSpeedRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedIncreaseInterval);
            _speed += speedIncrease;

            if (_speed >= speedToActivateRunMode)
            {
                if (!_isRunning)
                {
                    _isRunning = true;
                    onRun.Raise();
                }
            }
        }
    }

    public void ActivateWalkDebuff()
    {
        _speed = Mathf.Max(startSpeed, _speed / 2);

        if (_increaseSpeedRoutine != null)
        {
            StopCoroutine(_increaseSpeedRoutine);
            _increaseSpeedRoutine = StartCoroutine(IncreaseSpeedRoutine());
        }

        if (_isRunning && _speed < speedToActivateRunMode)
        {
            _isRunning = false;
            onWalk.Raise();
        }
    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        if (Mathf.Abs(_rigidbody.velocity.y) < 0.001f && !_isJumping)
        {
            _isJumping = true;
            _jumpTimeCounter = maxJumpTime;
            _rigidbody.AddForce(new Vector2(0, initialJumpForce), ForceMode2D.Impulse);
            onJump.Raise();
        }
    }

    public void OnJumpStarted()
    {
        if (Mathf.Abs(_rigidbody.velocity.y) < 0.001f && !_isJumping)
        {
            _isJumping = true;
            _jumpTimeCounter = maxJumpTime;
            _rigidbody.AddForce(new Vector2(0, initialJumpForce), ForceMode2D.Impulse);
            onJump.Raise();
        }
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        if (_isJumping)
        {
            _isJumping = false;
        }
    }

    public void OnJumpCanceled()
    {
        if (_isJumping)
        {
            _isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);

        if (_isJumping && _jumpTimeCounter > 0)
        {
            _rigidbody.AddForce(new Vector2(0, additionalJumpForce), ForceMode2D.Force);
            _jumpTimeCounter -= Time.fixedDeltaTime;
        }
        else
        {
            _isJumping = false;
        }

        _rigidbody.gravityScale = _rigidbody.velocity.y < 0 ? _gravityScale * fallGravityMultiplier : _gravityScale;
    }
}
