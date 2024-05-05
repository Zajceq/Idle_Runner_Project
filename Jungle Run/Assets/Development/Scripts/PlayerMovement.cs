using UnityEngine;
using UnityEngine.InputSystem;
using UnityAtoms.BaseAtoms;//Add this to use UnityAtoms inside script
using System.Collections; //Add this to use coroutines inside script

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float startSpeed = 5.0f;
    [SerializeField] private float speedIncrease = 0.5f;
    [SerializeField] private float speedIncreaseInterval = 10.0f;
    [SerializeField] private float speedToActivateRunMode = 5.0f;
    [SerializeField] private float jumpForce = 15.0f;

    [SerializeField] private InputActionReference jumpAction;

    [SerializeField] private VoidEvent onJump; // Jumping event
    [SerializeField] private VoidEvent onRun;
    [SerializeField] private VoidEvent onWalk;

    private float _speed;
    private bool _isRunning = false;
    private Rigidbody2D _rigidbody;
    private Coroutine _increaseSpeedRoutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _speed = startSpeed;
    }

    private void OnEnable()
    {
        // enable and subscribe actions so the player will respond on input
        jumpAction.action.Enable();
        jumpAction.action.performed += OnJump;

        _increaseSpeedRoutine = StartCoroutine(IncreaseSpeedRoutine());
    }

    private void OnDisable()
    {
        // unsubscribe and disable actions OnDisable to prevent bugs
        jumpAction.action.performed -= OnJump;
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
        _speed = Mathf.Max(startSpeed, _speed / 2); // divide the current speed in half or set it as startSpeed, whichever is greater.

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
        _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
    }
}
