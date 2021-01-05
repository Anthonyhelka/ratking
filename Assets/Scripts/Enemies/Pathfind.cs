using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Pathfind : MonoBehaviour
{
  private Rigidbody2D _rb;
  private Animator _animator;
  private Seeker _seeker;  
  private Transform _target;
  [SerializeField] private float _speed = 200.0f;
  [SerializeField] private float _nextWaypointDistance = 0.1f;
  private Path _path;
  private int _currentWaypoint = 0;
  private bool _reachedEndOfPath;
  [SerializeField] private float _chaseRange = 1.5f;

  [SerializeField] private bool _chasing;
  public bool Chasing {
    get { return _chasing; }
    set {
      if (value == _chasing) return;
      _chasing = value;
      _animator.SetBool("chasing", _chasing);
    }
  }

  void Awake() {
    _rb = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();
    _seeker = GetComponent<Seeker>();
    _target = GameObject.Find("Player").transform;
  }

  void Start() {
    InvokeRepeating("UpdatePath", 0.0f, 0.5f);
  }

  void UpdatePath() {
    if (_seeker.IsDone()) _seeker.StartPath(_rb.position, _target.position, OnPathComplete);
  }

  void OnPathComplete(Path nextPath) {
    if (!nextPath.error) {
      _path = nextPath;
      _currentWaypoint = 0;
    }
  }

  void FixedUpdate() {
    if (_path == null) return;

    if (_currentWaypoint >= _path.vectorPath.Count) {
      _reachedEndOfPath = true;
      return;
    } else {
      _reachedEndOfPath = false;
    }

    if (Chasing) {
      _chaseRange = 2.5f;
    } else {
      _chaseRange = 1.5f;
    }

    if (Vector2.Distance(_target.position, _rb.position) <= _chaseRange) {
      Chasing = true;
      CalculateMovement();
    } else {
      Chasing = false;
    }

    float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
    if (distance < _nextWaypointDistance) _currentWaypoint++;
  }

  void CalculateMovement() {
    Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
    Vector2 force = direction * _speed * Time.deltaTime;
    _rb.AddForce(force);
    Flip();
  }

  void Flip() {
    if (_rb.velocity.x >= 0.01f) {
      transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    } else if (_rb.velocity.x <= -0.01f) {
      transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
  }

  void OnDrawGizmosSelected() {
    Gizmos.DrawWireSphere(_rb.position, _chaseRange);
  }

  public void Stop() {
    _rb.velocity = new Vector2(0, 0);
    this.enabled = false;
  }
}
