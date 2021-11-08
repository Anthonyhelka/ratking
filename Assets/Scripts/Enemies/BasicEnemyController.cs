using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour {
  // Scripts
  private PlayerHealth _playerHealthScript;

  // Components
  private GameObject _alive;
  private Rigidbody2D _rb;
  private Animator _animator;

  // State
  private enum State { Moving, Knockback, Dead }
  [SerializeField] private State _currentState;

  // Detectors
  private bool _groundDetected, _wallDetected;
  [SerializeField] private Transform _groundCheck, _wallCheck, _touchDamageCheck;
  [SerializeField] private LayerMask _whatIsGround;
  [SerializeField] private float _groundCheckDistance, _wallCheckDistance;

  // Movement
  [SerializeField] private float _movementSpeed;
  private Vector2 _movement;
  [SerializeField] private int _facingDirection = 1;

  // Receiving Damage
  [SerializeField] private float  _maxHealth, _currentHealth;
  [SerializeField] private Vector2 _knockbackSpeed;
  [SerializeField] private float _knockbackDuration;
  private float _knockbackStartTime;
  private int _damageDirection;
  [SerializeField] private float _invulnerabilityDuration = 0.125f;
  private float _invulnerabilityTimer = -1.0f;
  [SerializeField] private GameObject _hitParticle, _bloodParticle;

  // Dealing Damage
  [SerializeField] private float _lastTouchDamageTime;
  [SerializeField] private float _touchDamageCooldown;
  [SerializeField] private float _touchDamage;
  [SerializeField] private float _touchDamageWidth;
  [SerializeField] private float _touchDamageHeight;
  [SerializeField] private LayerMask _whatIsPlayer;
  [SerializeField] private Vector2 _touchDamageTopRight;
  [SerializeField] private Vector2 _touchDamageBottomLeft;
  [SerializeField] private float[] _attackDetails = new float[2];

  void Awake() {
    _alive = transform.Find("Alive").gameObject;
    _rb = _alive.GetComponent<Rigidbody2D>();
    _animator = _alive.GetComponent<Animator>();
  }

  void Start() {
    _currentHealth = _maxHealth;
  }

  void Update() {
    switch(_currentState) {
      case State.Moving:
        UpdateMovingState();
        break;
      case State.Knockback:
        UpdateKnockbackState();
        break;
      case State.Dead:
        UpdateDeadState();
        break;
    }

    _invulnerabilityTimer = _invulnerabilityTimer + Time.deltaTime;
  }

  // Moving State
  void EnterMovingState() {

  }

  void UpdateMovingState() {
    _groundDetected = Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _whatIsGround);
    _wallDetected = Physics2D.Raycast(_wallCheck.position, Vector2.right, _wallCheckDistance, _whatIsGround);

    CheckTouchDamage();
    
    if (!_groundDetected || _wallDetected) {
      Flip();
    } else {
      _movement.Set(_movementSpeed * _facingDirection, _rb.velocity.y);
      _rb.velocity = _movement;
    }
  }

  void ExitMovingState() {

  }

  // Knockback State
  void EnterKnockbackState() {
    _knockbackStartTime = Time.time;
    _movement.Set(_knockbackSpeed.x * _damageDirection, _knockbackSpeed.y);
    _rb.velocity = _movement;
    _animator.SetBool("Knockback", true);
  }

  void UpdateKnockbackState() {
    if (Time.time >= _knockbackStartTime + _knockbackDuration) {
      SwitchState(State.Moving);
    }
  }

  void ExitKnockbackState() {
    _animator.SetBool("Knockback", false);
  }

  // Dead State
  void EnterDeadState() {
    Instantiate(_bloodParticle, _alive.transform.position, _bloodParticle.transform.rotation);
    Destroy(gameObject);
  }

  void UpdateDeadState() {

  }

  void ExitDeadState() {
      
  }

  // Other Functions

  void SwitchState(State state) {
    switch(_currentState) {
      case State.Moving:
        ExitMovingState();
        break;
      case State.Knockback:
        ExitKnockbackState();
        break;
      case State.Dead:
        ExitDeadState();
        break;
    }

    switch(state) {
      case State.Moving:
        EnterMovingState();
        break;
      case State.Knockback:
        EnterKnockbackState();
        break;
      case State.Dead:
        EnterDeadState();
        break;
    }

    _currentState = state;
  }

  void Flip() {
    _facingDirection *= -1;
    _alive.transform.Rotate(0.0f, 180.0f, 0.0f);
  }

  void Damage(float[] attackDetails) {
    if (_invulnerabilityTimer < _invulnerabilityDuration) { return; }

    _currentHealth -= attackDetails[0];
    _invulnerabilityTimer = 0.0f; 

    GameObject hitParticle = Instantiate(_hitParticle, _alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
    Destroy(hitParticle, 0.35f);

    if (attackDetails[1] > _alive.transform.position.x) {
      _damageDirection = -1;
    } else {
      _damageDirection = 1;
    }

    if (_currentHealth > 0.0f) {
      SwitchState(State.Knockback);
    } else if (_currentHealth <= 0.0f) {
      SwitchState(State.Dead);
    }
  }

  void CheckTouchDamage() {
    if (Time.time >= _lastTouchDamageTime + _touchDamageCooldown) {
      _touchDamageTopRight.Set(_touchDamageCheck.position.x + (_touchDamageWidth / 2), (_touchDamageCheck.position.y + (_touchDamageHeight / 2)));
      _touchDamageBottomLeft.Set(_touchDamageCheck.position.x - (_touchDamageWidth / 2), (_touchDamageCheck.position.y - (_touchDamageHeight / 2)));

      Collider2D hit = Physics2D.OverlapArea(_touchDamageBottomLeft, _touchDamageTopRight, _whatIsPlayer);

      if (hit != null) {
        _lastTouchDamageTime = Time.time;
        _attackDetails[0] = _touchDamage;
        _attackDetails[1] = _alive.transform.position.x;
        // _attackDetails[2] = gameObject.tag;
        hit.SendMessage("Damage", _attackDetails);
      }
    }
  }

  void OnDrawGizmos() {
    Gizmos.DrawLine(_groundCheck.position, new Vector2(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance));
    Gizmos.DrawLine(_wallCheck.position, new Vector2(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y));

    Vector2 botLeft = new Vector2(_touchDamageCheck.position.x - (_touchDamageWidth / 2), _touchDamageCheck.position.y - (_touchDamageHeight / 2));
    Vector2 botRight = new Vector2(_touchDamageCheck.position.x + (_touchDamageWidth / 2), _touchDamageCheck.position.y - (_touchDamageHeight / 2));
    Vector2 topRight = new Vector2(_touchDamageCheck.position.x + (_touchDamageWidth / 2), _touchDamageCheck.position.y + (_touchDamageHeight / 2));
    Vector2 topLeft = new Vector2(_touchDamageCheck.position.x - (_touchDamageWidth / 2), _touchDamageCheck.position.y + (_touchDamageHeight / 2));

    Gizmos.DrawLine(botLeft, botRight);
    Gizmos.DrawLine(botRight, topRight);
    Gizmos.DrawLine(topRight, topLeft);
    Gizmos.DrawLine(topLeft, botLeft);
  }
}
