﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  // Components
  private Rigidbody2D _rb;
  private BoxCollider2D _bc;
  private Animator _animator;
  private PlayerHealth _playerHealthScript;
  private PlayerCombat _playerCombatScript;

  // Movement
  private float _horizontalInput;
  private float _verticalInput;
  [SerializeField] private float _speed = 1.0f;
  public bool _facingRight = true;
  public bool _lockPlayerInput = false;

  // Jumping & Gravity
  private bool _isGrounded;
  [SerializeField] private LayerMask _groundLayerMask;
  [SerializeField] private LayerMask _enemyLayerMask;
  private bool _jumpRequest;
  private int _airJumpCount = 0;
  [SerializeField] private int _airJumpCountMax = 1;
  [SerializeField] private float _jumpForce = 3.5f;
  [SerializeField] private float _fallMultiplier = 2.5f;
  [SerializeField] private float _lowJumpMultiplier = 2.0f;
  
  // Dashing
  private bool _dashRequest;
  private int _dashCount;
  [SerializeField] private int _dashCountMax = 1;
  [SerializeField] private float _dashSpeed = 0.3f;
  private float _dashDurationCount;
  [SerializeField] private float _dashDurationCountMax = 0.4f;
  [SerializeField] private float _dashCooldown = 1.0f;
  private float _dashTimer = -1.0f;

  // Animation Variables
  [SerializeField] private bool _moving;
  public bool Moving {
    get { return _moving; }
    set {
      if (value == _moving) return;
      _moving = value;
      _animator.SetBool("moving", _moving);
    }
  }

  [SerializeField] private bool _jumping;
  public bool Jumping {
    get { return _jumping; }
    set {
      if (value == _jumping) return;
      _jumping = value;
      _animator.SetBool("jumping", _jumping);
    }
  }

  [SerializeField] private bool _doubleJump;
  public bool DoubleJumping {
    get { return _doubleJump; }
    set {
      if (value == _doubleJump) return;
      _doubleJump = value;
      _animator.SetBool("doubleJumping", _doubleJump);
    }
  }

  [SerializeField] private bool _falling;
  public bool Falling {
    get { return _falling; }
    set {
      if (value == _falling) return;
      _falling = value;
      _animator.SetBool("falling", _falling);
    }
  }

  [SerializeField] private bool _dashing;
  public bool Dashing {
    get { return _dashing; }
    set {
      if (value == _dashing) return;
      _dashing = value;
      _animator.SetBool("dashing", _dashing);
    }
  }

  void Awake() {
    _rb = GetComponent<Rigidbody2D>();
    _bc = GetComponent<BoxCollider2D>();
    _animator = GetComponent<Animator>();
    _playerHealthScript = GetComponent<PlayerHealth>();
    _playerCombatScript = GetComponent<PlayerCombat>();
    Application.targetFrameRate = 60;
    QualitySettings.vSyncCount = 0;
  }

  void Update() {
    if (!_lockPlayerInput) {
      GetInput();
    } else {
      ClearInput();
    }
    SetAnimations();
  }

  void GetInput() {
    // Movement
    _horizontalInput = Input.GetAxisRaw("Horizontal");
    _verticalInput = Input.GetAxisRaw("Vertical");
    
    // Jump
    if (Input.GetKey(KeyCode.Space)) {
      if (_isGrounded) {
        _jumpRequest = true;
      } else {
        if (Input.GetKeyDown(KeyCode.Space)) {
          if (_airJumpCount < _airJumpCountMax) {
            _jumpRequest = true;
            _airJumpCount++;
          }
        }
      }
    }
    
    // Dash
    if (Input.GetKeyDown(KeyCode.LeftShift) && _dashCount < _dashCountMax && Time.time >  _dashTimer) {
      _dashRequest = true;
    }

    // Attack
    if (Input.GetKeyDown(KeyCode.Mouse0)) {
      _playerCombatScript.Attack();
    }
  }

  void ClearInput() {
    _horizontalInput = 0.0f;
    _verticalInput = 0.0f;
    _jumpRequest = false;
    _dashRequest = false;
  }
  
  void SetAnimations() {
    // Clears Animation Variables
    ResetAnimationVariables();
    
    // Grounded Animations
    if (_isGrounded) {
      if (Mathf.Abs(_horizontalInput) > 0) {
        Moving = true;
      }
    } 
    
    // Airborne Animations
    if (!_isGrounded && !_playerHealthScript.Dying) {
      if (Mathf.Round(_rb.velocity.y) < 0) {
        Falling = true;
      } else if (Mathf.Round(_rb.velocity.y) > 0) {
        if (_airJumpCount > 0) {
          DoubleJumping = true;
        } else if (_airJumpCount == 0) {
          Jumping = true;
        }
      }
    }
  }

  public void ResetAnimationVariables() {
    Moving = false;
    Falling = false;
    Jumping = false;
    DoubleJumping = false;
  }

  void FixedUpdate() {
    // Movement & Gravity
    if (!Dashing && !_playerHealthScript.Damaged) {
      CalculateMovement();
      CalculateGravity();
    }

    // Detect Collisions With BoxCast
    GroundCheck();

    // Reset Values When Grounded
    if (_isGrounded) {
      _airJumpCount = 0;
      _dashCount = 0;
      if (!Dashing) {
        _dashDurationCount = 0;
      }
    }

    // User Requests
    if (_jumpRequest) {
      Jump();
      _jumpRequest = false;
    }
    if (_dashRequest) {
      Dash();
      _dashRequest = false;
    }
  }
  
  void CalculateMovement() {
    // Calculate Velocity
    _rb.velocity = new Vector2(_horizontalInput * _speed, _rb.velocity.y);

    // Flip Character
    if (_facingRight == false && _horizontalInput > 0) {
      Flip();
    } else if (_facingRight == true && _horizontalInput < 0) {
      Flip();
    }
  }

  void Flip() {
    _facingRight = !_facingRight;
    Vector3 Scaler = transform.localScale;
    Scaler.x *= -1;
    transform.localScale = Scaler;
  }

  void CalculateGravity() {
    // Fall Gravity
    if (_isGrounded && Mathf.Round(_rb.velocity.y) < 0) {
      _rb.gravityScale = _fallMultiplier;
      return;
    }

    // Low Jump Gravity
    if (Mathf.Round(_rb.velocity.y) > 0 && !Input.GetKey(KeyCode.Space) && !_playerHealthScript.Dying) {
      _rb.gravityScale = _lowJumpMultiplier;
      return;
    }

    // Normal Gravity
    _rb.gravityScale = 1.0f;
  }

  void Jump() {
    _rb.velocity = Vector2.up * 0;
    _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
  }

  void Dash() {
    StartCoroutine(DashRoutine());
  }

  IEnumerator DashRoutine() {
    Dashing = true;
    _lockPlayerInput = true;
    _dashCount++;

    // Remove Velocity & Stop Gravity
    _rb.velocity = new Vector2(0, 0) * 0;
    _rb.gravityScale = 0.0f;

    while (_dashDurationCount < _dashDurationCountMax) {
      _dashDurationCount += Time.deltaTime;
      if (_facingRight == true) {
        _rb.AddForce(Vector2.right * _dashSpeed, ForceMode2D.Impulse);
      } else {
        _rb.AddForce(Vector2.left * _dashSpeed, ForceMode2D.Impulse);
      }
      yield return 0;
    }
    Dashing = false;
    _lockPlayerInput = false;
    _dashTimer = Time.time + _dashCooldown;
  }

  void GroundCheck() {
    float height = 0.04f;
    RaycastHit2D groundcastHit = Physics2D.BoxCast(_bc.bounds.center, _bc.bounds.size, 0f, Vector2.down, 0.04f, _groundLayerMask);
    _isGrounded = groundcastHit.collider != null; 
  }
}
