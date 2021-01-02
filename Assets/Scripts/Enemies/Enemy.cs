using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour {
  [SerializeField] private HealthBar _healthBar;
  private Animator _animator;
  private BoxCollider2D _bc;
  [SerializeField] private GameObject _damagePopup;
  [SerializeField] private int _maxHealth = 100;
  [SerializeField] private int _currentHealth;

  void Awake()
  {
    _animator = GetComponent<Animator>();
    _bc = GetComponent<BoxCollider2D>();
    _currentHealth = _maxHealth;
    _healthBar.SetHealth(_currentHealth, _maxHealth);
  }

  public void TakeDamage(int damage) {
    _currentHealth -= damage;
    _healthBar.SetHealth(_currentHealth, _maxHealth);
    GameObject damagePopupInstance = Instantiate(_damagePopup, transform.position, Quaternion.identity);
    TextMeshPro damagePopupText = damagePopupInstance.transform.GetChild(0).GetComponent<TextMeshPro>();
    damagePopupText.SetText(damage.ToString());
    if (_currentHealth <= 0) {
      damagePopupText.color = Color.red;
      Die();
    }
  }

  void Die() {
    _healthBar.Destroy();
    Patrol patrol = GetComponent<Patrol>();
    if (patrol != null) patrol.enabled = false;
    _bc.enabled = false;
    _animator.SetBool("isDead", true);
    Destroy (gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
  }
}
