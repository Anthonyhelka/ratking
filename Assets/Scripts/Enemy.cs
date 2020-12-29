using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  private Animator _animator;
  private BoxCollider2D _bc;
  public int maxHealth = 1;
  private int currentHealth;

  void Awake()
  {
    _animator = GetComponent<Animator>();
    _bc = GetComponent<BoxCollider2D>();
    currentHealth = maxHealth;
  }

  void Update()
  {
    
  }

  public void TakeDamage(int damage) {
    currentHealth -= damage;
    Debug.Log(currentHealth);
    if (currentHealth <= 0) {
      Die();
    }
  }

  void Die() {
    _animator.SetBool("isDead", true);
    _bc.enabled = false;
    Patrol patrol = GetComponent<Patrol>();
    if (patrol != null) {
      patrol.enabled = false;
    }
    Destroy (gameObject, _animator.GetCurrentAnimatorStateInfo(0).length);
  }
}
