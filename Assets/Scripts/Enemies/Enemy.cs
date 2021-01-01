using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour {
  private Animator _animator;
  private BoxCollider2D _bc;
  [SerializeField] private GameObject _damagePopup;
  [SerializeField] private int maxHealth = 1;
  [SerializeField] private int currentHealth;

  void Awake()
  {
    _animator = GetComponent<Animator>();
    _bc = GetComponent<BoxCollider2D>();
    currentHealth = maxHealth;
  }

  public void TakeDamage(int damage) {
    currentHealth -= damage;
    GameObject damagePopupInstance = Instantiate(_damagePopup, transform);
    TextMeshPro damagePopupText = damagePopupInstance.transform.GetChild(0).GetComponent<TextMeshPro>();
    damagePopupText.SetText(damage.ToString());
    if (currentHealth <= 0) {
      damagePopupText.color = Color.red;
      Die();
    }
  }

  void Die() {
    Patrol patrol = GetComponent<Patrol>();
    if (patrol != null) patrol.enabled = false;
    _bc.enabled = false;
    _animator.SetBool("isDead", true);
    Destroy (gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
  }
}
