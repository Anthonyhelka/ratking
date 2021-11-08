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
  private bool _danceRequest;
  public IEnumerator _danceRoutine;

  // Jumping & Gravity
  public bool _isGrounded;
  [SerializeField] private LayerMask _groundLayerMask;
  [SerializeField] private LayerMask _enemyLayerMask;
  private bool _jumpRequest;
  private int _airJumpCount = 0;
  [SerializeField] private int _airJumpCountMax = 1;
  [SerializeField] private float _jumpForce = 3.5f;
  [SerializeField] private float _bounceForce = 3.5f;
  [SerializeField] public float _bounceDuration = 3.0f;
  [SerializeField] public float _bounceDurationMax = 3.0f;
  public bool finalBounce = false;
  [SerializeField] private float _fallMultiplier = 2.5f;
  [SerializeField] private float _lowJumpMultiplier = 2.0f;
  [SerializeField] private bool _queueRoll;
  [SerializeField] private float _rollDuration = 0.4f;
  public IEnumerator _rollRoutine;

  // Dashing
  private bool _dashRequest;
  public int _dashCount;
  public int _dashCountMax = 1;
  [SerializeField] private float _dashHorizontalSpeed = 8.0f;
  [SerializeField] private float _dashDurationCountMax = 0.4f;
  public float _dashCooldown = 1.0f;
  public float _dashTimer = -1.0f;
  public IEnumerator _dashRoutine;
  
  // Knockback
  [SerializeField] private float _knockbackStartTime;
  [SerializeField] private float _knockbackDuration;
  [SerializeField] private Vector2 _knockbackSpeed;

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

  [SerializeField] private bool _rolling;
  public bool Rolling {
    get { return _rolling; }
    set {
      if (value == _rolling) return;
      _rolling = value;
      _animator.SetBool("rolling", _rolling);
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

  [SerializeField] public bool _knockback;
  public bool Knockback {
    get { return _knockback; }
    set {
      if (value == _knockback) return;
      _knockback = value;
      _animator.SetBool("knockback", _knockback);
    }
  }

  [SerializeField] private bool _sleepingOne;
  public bool SleepingOne {
    get { return _sleepingOne; }
    set {
      if (value == _sleepingOne) return;
      _sleepingOne = value;
      _animator.SetBool("sleepingOne", _sleepingOne);
    }
  }

  [SerializeField] private bool _sleepingTwo;
  public bool SleepingTwo {
    get { return _sleepingTwo; }
    set {
      if (value == _sleepingTwo) return;
      _sleepingTwo = value;
      _animator.SetBool("sleepingTwo", _sleepingTwo);
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

  [SerializeField] private bool _dancing;
  public bool Dancing {
    get { return _dancing; }
    set {
      if (value == _dancing) return;
      _dancing = value;
      _animator.SetBool("dancing", _dancing);
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

    // Dance 
    if (Input.GetButton("Dance")) {
      _danceRequest = true;
    }
  }

  void ClearInput() {
    _horizontalInput = 0.0f;
    _verticalInput = 0.0f;
    _jumpRequest = false;
    _dashRequest = false;
    _danceRequest = false;
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
    if (!Dashing && !Knockback) {
      CalculateMovement();
      CalculateLookAround();
      if (!_playerCombatScript.Attacking || _playerCombatScript.AirLightAttack) CalculateGravity();
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
      _bounceDuration = _bounceDurationMax;
      finalBounce = false;
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
    if (_danceRequest) {
      Dance();
      _danceRequest = false;
    }

    CheckKnockback();
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
    if (_isGrounded && Mathf.Round(_rb.velocity.y) < 0 && !_playerCombatScript.AirLightAttack) {
      _rb.gravityScale = _fallMultiplier;
      return;
    }

    // Low Jump Gravity
    if (Mathf.Round(_rb.velocity.y) > 0 && !Input.GetButton("Jump") && !_playerHealthScript.Dying && !_playerCombatScript.AirLightAttack) {
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

  public void Bounce() {
    CreateDust();
    _rb.velocity = Vector2.up * 0;
    _rb.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);

    if (_bounceDuration <= 0.0f) {
      finalBounce = true;
    }
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
      if (Knockback || _playerHealthScript.Dying) break;
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

  void Dance() {
    _danceRoutine = DanceRoutine();
    StartCoroutine(_danceRoutine);
  }

  IEnumerator DanceRoutine() {
    Dancing = true;
    while (Mathf.Round(_horizontalInput) == 0.0f && Mathf.Round(_rb.velocity.x) == 0.0f && Mathf.Round(_rb.velocity.y) == 0.0f) {
      yield return 0;
    }
    Dancing = false;
  }

  public void Roll() {
    _rollRoutine = RollRoutine();
    StartCoroutine(_rollRoutine);
  }

  IEnumerator RollRoutine() {
    Rolling = true;
    float duration = 0.0f;
    while (duration < _rollDuration) {
      if (_horizontalInput == 0.0f) { break; }
      _speed = 1.5f;
      duration += Time.deltaTime;
      yield return 0;
    }
    _speed = 1.0f;
    Rolling = false;
  }

  void GroundCheck() {
    float height = 0.03f;
    RaycastHit2D groundcastHit = Physics2D.BoxCast(_bc.bounds.center, _bc.bounds.size, 0f, Vector2.down, height, _groundLayerMask);
    _isGrounded = groundcastHit.collider != null; 
  }

  void DetermineLockedInput() {
    if (Dashing || (_playerCombatScript.Attacking && !_playerCombatScript.AirLightAttack && !_playerCombatScript.AirHeavyAttack) || Knockback || _playerHealthScript.Dying) {
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
    while (_isIdle && !Dancing) {
      if (Random.value > 0.5) {
        SleepingOne = true;
        yield return new WaitForSeconds(1.4f);
      } else {
        SleepingTwo = true;
        yield return new WaitForSeconds(3.0f);
      }
      SleepingOne = false;
      SleepingTwo = false;
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

  public void doKnockback(int direction) {
    Knockback = true;
    _knockbackStartTime = Time.time;
    _rb.velocity = new Vector2(_knockbackSpeed.x * direction, _knockbackSpeed.y);
  }

  void CheckKnockback() {
    if (Time.time >= _knockbackStartTime + _knockbackDuration && Knockback) {
      Knockback = false;
      _rb.velocity = new Vector2(0.0f, _rb.velocity.y);
    }
  }
}
