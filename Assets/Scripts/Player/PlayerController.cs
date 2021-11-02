using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour {

  // Components
  private Rigidbody2D _rb;
  private BoxCollider2D _bc;
  private Animator _animator;
  private PlayerHealth _playerHealthScript;
  private PlayerCombat _playerCombatScript;
  private CinemachineVirtualCamera _cinemaMachineVirtualCamera;
  private CinemachineFramingTransposer _cinemaMachineFramingTransposer;

  // Movement
  private float _horizontalInput;
  private float _verticalInput;
  [SerializeField] private float _speed = 1.0f;
  public bool _facingRight = true;
  public bool _lockPlayerInput = false;
  private float _lookUpTimer = 0.0f;
  private float _lookDownTimer = 0.0f;
  [SerializeField] private float _lookDuration = 1.0f;
  private bool _hasTouchedGround = false;
  private float _idleTime = 0.0f;
  private IEnumerator _idleRoutine;
  private bool _isIdle = false;
  [SerializeField] private ParticleSystem _dust;

  // Jumping & Gravity
  public bool _isGrounded;
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
  public int _dashCount;
  public int _dashCountMax = 1;
  [SerializeField] private float _dashHorizontalSpeed = 8.0f;
  [SerializeField] private float _dashDurationCountMax = 0.4f;
  public float _dashCooldown = 1.0f;
  public float _dashTimer = -1.0f;
  public IEnumerator _dashRoutine;
  
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

  [SerializeField] private bool _sleeping;
  public bool Sleeping {
    get { return _sleeping; }
    set {
      if (value == _sleeping) return;
      _sleeping = value;
      _animator.SetBool("sleeping", _sleeping);
    }
  }

  [SerializeField] private bool _lookingUp;
  public bool LookingUp {
    get { return _lookingUp; }
    set {
      if (value == _lookingUp) return;
      _lookingUp = value;
      _animator.SetBool("lookingUp", _lookingUp);
    }
  }

  [SerializeField] private bool _lookingDown;
  public bool LookingDown {
    get { return _lookingDown; }
    set {
      if (value == _lookingDown) return;
      _lookingDown = value;
      _animator.SetBool("lookingDown", _lookingDown);
    }
  }

  void Awake() {
    _rb = GetComponent<Rigidbody2D>();
    _bc = GetComponent<BoxCollider2D>();
    _animator = GetComponent<Animator>();
    _playerHealthScript = GetComponent<PlayerHealth>();
    _playerCombatScript = GetComponent<PlayerCombat>();
    _playerCombatScript = GetComponent<PlayerCombat>();
    _cinemaMachineVirtualCamera = GameObject.Find("Cinemachine_Camera").GetComponent<CinemachineVirtualCamera>();
    _cinemaMachineFramingTransposer = _cinemaMachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    Application.targetFrameRate = 60;
    QualitySettings.vSyncCount = 0;
  }

  void Update() {
    DetermineLockedInput();
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

    if (Input.anyKey) {
      _idleTime = 0.0f;
    }

    // Jump
    if (Input.GetButton("Jump") && !_playerCombatScript.AirHeavyAttack) {
      if (_isGrounded) {
        _jumpRequest = true;
      } else {
        if (Input.GetButtonDown("Jump") && !_playerCombatScript.AirHeavyAttack) {
          if (_airJumpCount < _airJumpCountMax) {
            _jumpRequest = true;
            _airJumpCount++;
          }
        }
      }
    }
    
    // Dash
    if (Input.GetButton("Dash") && _dashCount < _dashCountMax && Time.time >  _dashTimer && !_playerCombatScript.AirHeavyAttack) {
      _dashRequest = true;
    }

    // Attack
    if (Input.GetButton("Fire1") && !_playerCombatScript.Attacking) {
      _playerCombatScript.Attack("Light");
    } else if (Input.GetButton("Fire2") && !_playerCombatScript.Attacking) {
      _playerCombatScript.Attack("Heavy");
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
      CalculateLookAround();
      if (!_playerCombatScript.Attacking) CalculateGravity();
    }

    // Detect Collisions With BoxCast
    GroundCheck();

    _idleTime += Time.deltaTime;
    if (_idleTime > 10.0f) {
      if (!_isIdle) Idle();
    } else {
      _isIdle = false;
    }

    // Reset Values When Grounded
    if (_isGrounded) {
      _airJumpCount = 0;
      _hasTouchedGround = true;
    }
    if (Time.time > _dashTimer && _hasTouchedGround) _dashCount = 0;

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

  public void Flip() {
    _facingRight = !_facingRight;
    Vector3 Scaler = transform.localScale;
    Scaler.x *= -1;
    transform.localScale = Scaler;
    if (_isGrounded) CreateDust();
  }

  void CalculateGravity() {
    // Fall Gravity
    if (_isGrounded && Mathf.Round(_rb.velocity.y) < 0) {
      _rb.gravityScale = _fallMultiplier;
      return;
    }

    // Low Jump Gravity
    if (Mathf.Round(_rb.velocity.y) > 0 && !Input.GetButton("Jump") && !_playerHealthScript.Dying) {
      _rb.gravityScale = _lowJumpMultiplier;
      return;
    }

    // Normal Gravity
    _rb.gravityScale = 1.0f;
  }

  void CalculateLookAround() {
    if (_horizontalInput != 0.0f || _rb.velocity.x != 0.0f || _rb.velocity.y != 0.0f) {
      _lookUpTimer = 0.0f;
      _lookDownTimer = 0.0f;
      LookingUp = false;
      LookingDown = false; 
      _cinemaMachineFramingTransposer.m_TrackedObjectOffset.y = 0.0f;
      return;
    }
    if (_verticalInput > 0.0f) {
      _lookDownTimer = 0.0f;
      _lookUpTimer += Time.deltaTime;
      if (_lookUpTimer > _lookDuration) {
        LookingUp = true;
        _cinemaMachineFramingTransposer.m_TrackedObjectOffset.y = 1.0f;
      }
    } else if (_verticalInput < 0.0f) {
      _lookUpTimer = 0.0f;
      _lookDownTimer += Time.deltaTime;
      if (_lookDownTimer > _lookDuration) {
        LookingDown = true;
        _cinemaMachineFramingTransposer.m_TrackedObjectOffset.y = -1.0f;
      }
    } else {
      _lookUpTimer = 0.0f;
      _lookDownTimer = 0.0f;
      LookingUp = false;
      LookingDown = false;
      _cinemaMachineFramingTransposer.m_TrackedObjectOffset.y = 0.0f;
    }
  }

  void Jump() {
    CreateDust();
    _rb.velocity = Vector2.up * 0;
    _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
  }

  void Dash() {
    _dashRoutine = DashRoutine();
    StartCoroutine(_dashRoutine);
  }

  IEnumerator DashRoutine() {
    Dashing = true;
    _dashCount++;
    _rb.velocity = new Vector2(0, 0);
    
    float duration = 0.0f;
    bool queueLightAttack = false;
    bool queueHeavyAttack = false;
    _dashTimer = 10000000.0f;
    Vector2 velocity;
    if (_facingRight == true) {
      velocity = new Vector2(1.0f, 0.0f) * _dashHorizontalSpeed;
    } else {
      velocity = new Vector2(-1.0f, 0.0f) * _dashHorizontalSpeed;
    }

    while (duration <= _dashDurationCountMax) {
      if (_playerHealthScript.Damaged || _playerHealthScript.Dying) break;
      if (Input.GetButtonDown("Fire1") && duration > 0.05f && Time.time > _playerCombatScript._attackTimer) {
        queueLightAttack = true;
        break;
      } else if (Input.GetButtonDown("Fire2") && duration > 0.05f && Time.time > _playerCombatScript._attackTimer) {
        queueHeavyAttack = true;
        break;
      }
      duration += Time.deltaTime;
      _rb.gravityScale = 0.0f;
      _rb.AddForce(velocity * Time.deltaTime, ForceMode2D.Impulse);
      yield return 0;
    }

    transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

    _hasTouchedGround = false;
    _dashTimer = Time.time + _dashCooldown;

    Dashing = false;

    if (queueLightAttack) _playerCombatScript.Attack("Light");
    if (queueHeavyAttack) _playerCombatScript.Attack("Heavy");
  }

  void GroundCheck() {
    float height = 0.04f;
    RaycastHit2D groundcastHit = Physics2D.BoxCast(_bc.bounds.center, _bc.bounds.size, 0f, Vector2.down, height, _groundLayerMask);
    _isGrounded = groundcastHit.collider != null; 
  }

  void DetermineLockedInput() {
    if (Dashing || (_playerCombatScript.Attacking && !_playerCombatScript.AirLightAttack && !_playerCombatScript.AirHeavyAttack) || _playerHealthScript.Damaged || _playerHealthScript.Dying) {
      _lockPlayerInput = true;
    } else {
      _lockPlayerInput = false;
    }
  }

  void Idle() {
    _idleRoutine = IdleRoutine();
    StartCoroutine(_idleRoutine);
  }

  IEnumerator IdleRoutine() {
    _isIdle = true;
    while (_isIdle) {
      Sleeping = true;
      yield return new WaitForSeconds(1.4f);
      Sleeping = false;
      yield return new WaitForSeconds(5.0f);
    }
    _isIdle = false;
  }

  void CreateDust() {
    var dustVelocityOverLifeTime = _dust.velocityOverLifetime;
    if (_rb.velocity.normalized.x == 0.0f) {
      dustVelocityOverLifeTime.x = 0.0f;
    } else if (_facingRight) {
      dustVelocityOverLifeTime.x = -0.2f;
    } else {
      dustVelocityOverLifeTime.x = 0.2f;
    }
    _dust.Play();
  }
}
