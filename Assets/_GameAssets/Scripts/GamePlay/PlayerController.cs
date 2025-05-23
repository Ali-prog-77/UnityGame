using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
  public event Action OnPlayerJumped;

  public event Action<PlayerState> OnPlayerStateChanged;

  [Header("Referances")]
  [SerializeField] private Transform _orientationTransform;

  [Header("Movement Settings")]

  [SerializeField] private KeyCode _movementKey;
  [SerializeField] private float _movementSpeed;

  [Header("Jump Settings")]

  [SerializeField] private KeyCode _jumpKey;

  [SerializeField] private float _jumpForce;

  [SerializeField] private bool _canjump;

  [SerializeField] private float _jumpCooldown;

  [SerializeField] private float _airMultiplier;

  [SerializeField] private float _airDrag;

  [Header("Ground Check Settings")]

  [SerializeField] private float _playerHeight;

  [SerializeField] private LayerMask _grounLayer;

  [SerializeField] private float _groundDrag;


  [Header("Sliding Settings")]

  [SerializeField] private KeyCode _slideKey;
  [SerializeField] private float _slideMultiplier;

  [SerializeField] private float _slideDrag;

  private StateController _stateController;

  public Rigidbody _playerRigidbody;

  private float _startingMovementSpeed, _startingJumpForce;
  private float _horizonalInput, _verticalInput;

  private Vector3 _movementDirection;

  private bool _isSliding;
  private void Awake()
  {
    _stateController = GetComponent<StateController>();
    _playerRigidbody = GetComponent<Rigidbody>();
    _playerRigidbody.freezeRotation = true;

    _startingMovementSpeed = _movementSpeed;
    _startingJumpForce = _jumpForce;
  }

  private void Update()
  {
    SetInputs();
    SetStates();
    SetPlayerDrag();
    LimitPlayerSpeed();
  }

  private void FixedUpdate()
  {
    SetPlayerMovement();
  }


  private void SetInputs()
  {
    _horizonalInput = Input.GetAxisRaw("Horizontal");
    _verticalInput = Input.GetAxisRaw("Vertical");

    if (Input.GetKeyDown(_slideKey))
    {
      _isSliding = true;
      Debug.Log("Player Sliding!");
    }
    else if (Input.GetKeyDown(_movementKey))

    {
      _isSliding = false;
      Debug.Log("Player Moving!");
    }
    else if (Input.GetKey(_jumpKey) && _canjump && IsGround())
    {
      _canjump = false;
      SetJumping();
      Invoke(nameof(ResetJumping), _jumpCooldown);
    }
  }

  public void SetStates()
  {
    var issliding = IsSliding();
    var movementDirection = GetMovementDirection();
    var isGrounded = IsGround();
    var currentState = _stateController.GetCurrentState();

    var newState = currentState switch
    {
      _ when movementDirection == Vector3.zero && isGrounded && !issliding => PlayerState.Idle,
      _ when movementDirection != Vector3.zero && isGrounded && !issliding => PlayerState.Move,
      _ when movementDirection != Vector3.zero && isGrounded && issliding => PlayerState.Slide,
      _ when movementDirection == Vector3.zero && isGrounded && issliding => PlayerState.SlideIdle,
      _ when !_canjump && !isGrounded => PlayerState.Jump,
      _ => currentState,
    };

    if (newState != currentState)
    {
      _stateController.ChangeState(newState);
      OnPlayerStateChanged?.Invoke(newState);
    }

    Debug.Log(newState);
  }

  private void SetPlayerMovement()
  {
    _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizonalInput;

    float forceMultiplier = _stateController.GetCurrentState() switch
    {
      PlayerState.Move => 1f,
      PlayerState.Slide => _slideMultiplier,
      PlayerState.Jump => _airMultiplier,
      _ => 1f,
    };

    _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);



  }

  private void SetPlayerDrag()
  {

    _playerRigidbody.linearDamping = _stateController.GetCurrentState() switch
    {
      PlayerState.Move => _groundDrag,
      PlayerState.Slide => _slideDrag,
      PlayerState.Jump => _airDrag,
      _ => _playerRigidbody.linearDamping
    };

    if (_isSliding)
    {
      _playerRigidbody.linearDamping = _slideDrag;
    }
    else
    {
      _playerRigidbody.linearDamping = _groundDrag;
    }
  }

  private void LimitPlayerSpeed()
  {
    Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
    if (flatVelocity.magnitude > _movementSpeed)
    {
      Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
      _playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, _playerRigidbody.linearVelocity.y, limitedVelocity.z);
    }
  }

  private void SetJumping()
  {
    OnPlayerJumped?.Invoke();
    _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
    _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
  }

  private void ResetJumping()
  {
    _canjump = true;
  }

  private bool IsGround()
  {
    return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _grounLayer);
  }

  private Vector3 GetMovementDirection()
  {
    return _movementDirection.normalized;
  }

  private bool IsSliding()
  {
    return _isSliding;
  }

  public void SetMovementSpeed(float speed, float duration)
  {
    _movementSpeed += speed;
    Invoke(nameof(ResetMovementSpeed), duration);
  }

  private void ResetMovementSpeed()
  {
    _movementSpeed = _startingMovementSpeed;
  }

  public void SetJumpForce(float force, float duration)
  {
    _jumpForce += force;
    Invoke(nameof(ResetJumpForce), duration);

  }

  private void ResetJumpForce()
  {
    _jumpForce = _startingJumpForce;
  }

}
