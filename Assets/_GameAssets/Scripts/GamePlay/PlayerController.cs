using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
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

  [Header("Ground Check Settings")]

  [SerializeField] private float _playerHeight;

  [SerializeField] private LayerMask _grounLayer;

  [SerializeField] private float _groundDrag;


  [Header("Sliding Settings")]

  [SerializeField] private KeyCode _slideKey;
  [SerializeField] private float _slideMultiplier;

  [SerializeField] private float _slideDrag;

  private Rigidbody _playerRigidbody;

  private float _horizonalInput, _verticalInput;

  private Vector3 _movementDirection;

  private bool _isSliding;
  private void Awake()
  {
    _playerRigidbody = GetComponent<Rigidbody>();
    _playerRigidbody.freezeRotation = true;
  }

  private void Update()
  {
    SetInputs();
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

  private void SetPlayerMovement()
  {
    _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizonalInput;

    if (_isSliding)
    {
      _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed * _slideMultiplier, ForceMode.Force);
    }
    else
    {
      _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed, ForceMode.Force);

    }
  }

  private void SetPlayerDrag()
  {
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
    Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x,0f,_playerRigidbody.linearVelocity.z);
    if(flatVelocity.magnitude > _movementSpeed)
    {
      Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
      _playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x,_playerRigidbody.linearVelocity.y, limitedVelocity.z);
    } 
  }

  private void SetJumping()
  {
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
}
