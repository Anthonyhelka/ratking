using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour {
  [SerializeField] private HealthBar _healthBar;
  private Animator _animator;
  private BoxCollider2D _hitbox;
  private Rigidbody2D _rb;
  private SpriteRenderer _sr;
  private GameObject _player;
  [SerializeField] private GameObject _damagePopup;
  private Patrol _patrolScript;
  private Pathfind _pathfindScript;
  [SerializeField] private int _maxHealth = 100;
  [SerializeField] private int _currentHealth;
  private IEnumerator _damageRoutine;
  [SerializeField] private float _deathAnimationDuration;
  [SerializeField] private float _knockbackDuration = 0.2f;
  [SerializeField] private float _knockbackForce = 5.0f;
  public bool airEnemy;

  void Awake()
  {
    _animator = GetComponent<Animator>();
    _hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    _rb = GetComponent<Rigidbody2D>();
    _sr = GetComponent<SpriteRenderer>();
    _patrolScript = GetComponent<Patrol>();
    _pathfindScript = GetComponent<Pathfind>();
    _player = GameObject.Find("Player");
    _currentHealth = _maxHealth;
    _healthBar.SetHealth(_currentHealth, _maxHealth);
  }

  public void TakeDamage(int damage) {
    _currentHealth -= damage;
    _healthBar.SetHealth(_currentHealth, _maxHealth);
    GameObject damagePopupInstance = Instantiate(_damagePopup, transform.position, Quaternion.identity);
    TextMeshPro damagePopupText = damagePopupInstance.transform.GetChild(0).GetComponent<TextMeshPro>();
    damagePopupText.SetText(damage.ToString());
    _damageRoutine = DamageRoutine();
    if (_currentHealth <= 0) {
      damagePopupText.color = Color.red;
      Die();
    } else {
      StartCoroutine(_damageRoutine); 
    }
  }

  IEnumerator DamageRoutine() {
    ToggleScripts(false);
    _sr.color = Color.red;
    Vector2 direction = (transform.position - _player.transform.position);
    direction = direction.normalized;
    direction = new Vector2(direction.x, 0.0f);
    _rb.velocity = new Vector2(0, 0) * 0;
    float duration = 0.0f;
    while (duration < _knockbackDuration) {
      duration += Time.deltaTime;
      _rb.AddForce(direction * _knockbackForce * Time.deltaTime, ForceMode2D.Impulse);
      yield return 0;
    }
    _rb.velocity = new Vector2(0, 0) * 0;
    ToggleScripts(true);
    if (_patrolScript) {
      if (direction.x > 0.0f) {
        _patrolScript._movingRight = false;
      } else {
        _patrolScript._movingRight = true;
      }
    }
    _sr.color = Color.white;
  }

  void Die() {
    _healthBar.Destroy();

    Patrol patrol = GetComponent<Patrol>();
    if (patrol != null) patrol.Stop();
    Pathfind pathfind = GetComponent<Pathfind>();
    if (pathfind != null) pathfind.Stop();

    _hitbox.enabled = false;
    _rb.bodyType = RigidbodyType2D.Kinematic;
    transform.rotation = Quaternion.Euler(0, 0, 0);
    _animator.SetBool("isDead", true);
    Destroy (gameObject, _deathAnimationDuration);
  }

  void ToggleScripts(bool status) {
    if (_patrolScript) _patrolScript.enabled = status;
    if (_pathfindScript) _pathfindScript.enabled = status;
  }
}
