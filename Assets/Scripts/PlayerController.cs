using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D _rb;
    private Animator _animator;

    // Movement
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField] private float _speed = 1.0f;
    private bool _facingRight = true;
    private enum State { normal, jumping, dashing };
    [SerializeField] private State _state;
    // Jumping
    private bool _jumpRequest;
    [SerializeField] private int _jumpsValue = 2;
    private int _jumps;
    [SerializeField] private float _jumpForce = 3.5f;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2.0f;
    // Dashing
    private bool _dashRequest;
    [SerializeField] private int _dashesValue = 1;
    private int _dashes;
    [SerializeField] private float _dashSpeed = 0.3f;
    private float _dashDuration;
    [SerializeField] private float _dashDurationValue = 0.5f;
    [SerializeField] private float _dashCooldown = 1.0f;
    private float _dashTimer = -1.0f;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    void Start() {   
        _jumps = _jumpsValue;
        _dashes = _dashesValue;
        _dashDuration = _dashDurationValue;
    }

    void Update() {
        GetInput();
    }

    void FixedUpdate() {
        if (_jumpRequest) {
            Jump();
            _jumpRequest = false;
        }
        if (_dashRequest) {
            Dash();
            _dashRequest = false;
        }
        CalculateMovement();
        CalculateGravity();
    }

    void GetInput() {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        _animator.SetFloat("Speed", Mathf.Abs(_horizontalInput));
        if (Input.GetKeyDown(KeyCode.Space) && _jumps > 0 && _state != State.dashing) {
            _jumpRequest = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && _dashes > 0 && Time.time >  _dashTimer) {
            _dashRequest = true;
        }
    }

    void CalculateMovement() {
        if (_state != State.dashing) {
            _rb.velocity = new Vector2(_horizontalInput * _speed, _rb.velocity.y);
            _animator.SetFloat("Speed", Mathf.Abs(_horizontalInput));
            if (_facingRight == false && _horizontalInput > 0) {
                Flip();
            } else if (_facingRight == true && _horizontalInput < 0) {
                Flip();
            }
        }
    }

    void CalculateGravity() {
        if (Mathf.Round(_rb.velocity.y) < 0) {
            _animator.SetBool("Falling", true);
            _rb.gravityScale = _fallMultiplier;
        } else if (Mathf.Round(_rb.velocity.y) > 0 && !Input.GetKey(KeyCode.Space)) {
            _animator.SetBool("Falling", false);
            _rb.gravityScale = _lowJumpMultiplier;
        } else if (_state == State.dashing) {
            _animator.SetBool("Falling", false);
            _rb.gravityScale = 0.0f;
        } else {
            _animator.SetBool("Falling", false);
            _rb.gravityScale = 1.0f;
        }
    }

    void Flip() {
        _facingRight = !_facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Jump() {
        _animator.SetBool("Falling", false);
        if (_jumps < _jumpsValue) {
            _animator.SetBool("Jumping", false);
            _animator.SetBool("Double Jumping", true);
        } else {
            _animator.SetBool("Jumping", true);
        }
        _jumps--;
        _state = State.jumping;
        _rb.velocity = Vector2.up * 0;
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    void Dash() {
        StartCoroutine(DashRoutine());
    }

    IEnumerator DashRoutine() {
        _animator.SetBool("Falling", false);
        _animator.SetBool("Jumping", false);
        _animator.SetBool("Dashing", true);
        State entryState = _state;        
        _state = State.dashing;
        _dashes--;
        _rb.velocity = new Vector2(0, 0) * 0;
        while (_dashDuration > 0) {
            _dashDuration -= Time.deltaTime;
            if (_facingRight == true) {
                _rb.AddForce(Vector2.right * _dashSpeed, ForceMode2D.Impulse);
            } else {
                _rb.AddForce(Vector2.left * _dashSpeed, ForceMode2D.Impulse);
            }
            yield return 0;
        }
        if (_dashes > 0) {
            _dashDuration = _dashDurationValue;
        }
        if (entryState == State.normal) {
            _state = State.normal;
            _dashDuration = _dashDurationValue;
            _dashes++;
        } else if (entryState == State.jumping) {
            _state = State.jumping;
        }
        _dashTimer = Time.time + _dashCooldown;
        _animator.SetBool("Dashing", false);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy") {
            if (_state == State.jumping || _state == State.dashing) {
                _animator.SetBool("Falling", false);
                _animator.SetBool("Jumping", false);
                _animator.SetBool("Double Jumping", false);
                _animator.SetBool("Dashing", false);
                _jumps = _jumpsValue;
                _dashDuration = _dashDurationValue;
                _dashes = _dashesValue;
                _state = State.normal;
            }
        }
    }
}