using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  private Animator _animator;
  private BoxCollider2D _bc;
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
    if (currentHealth <= 0) Die();
  }

  void Die() {
    Patrol patrol = GetComponent<Patrol>();
    if (patrol != null) patrol.enabled = false;
    _bc.enabled = false;
    _animator.SetBool("isDead", true);
    Destroy (gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
  }
}
