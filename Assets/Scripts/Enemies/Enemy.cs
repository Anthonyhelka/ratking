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
  [SerializeField] private GameObject _damagePopup;
  [SerializeField] private int _maxHealth = 100;
  [SerializeField] private int _currentHealth;
  private IEnumerator _damageRoutine;
  [SerializeField] private float _damageFlashDuration = 0.1f;
  [SerializeField] private float _deathAnimationDuration;
  public bool airEnemy;

  void Awake()
  {
    _animator = GetComponent<Animator>();
    _hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    _rb = GetComponent<Rigidbody2D>();
    _sr = GetComponent<SpriteRenderer>();
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
    StartCoroutine(_damageRoutine);
    if (_currentHealth <= 0) {
      damagePopupText.color = Color.red;
      Die();
    }
  }

  IEnumerator DamageRoutine() {
    _sr.color = Color.red;
    yield return new WaitForSeconds(_damageFlashDuration);
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
}
