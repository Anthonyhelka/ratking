using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
  private Animator _animator;
  public Transform attackPoint;
  public float attackRange = 0.1f;
  public LayerMask enemyLayers;
  public int attackDamage = 1;
  public float attackCooldown = 1.0f;
  private float _nextAttackTime = -1.0f;

  void Awake()
  {
    _animator = GetComponent<Animator>();
  }

  void Update()
  {
    if (Time.time >= _nextAttackTime) {
      if (Input.GetKeyDown(KeyCode.Mouse0)) {
        Attack();
        _nextAttackTime = Time.time + attackCooldown;
      }
    }
  }

  void Attack() {
    _animator.SetTrigger("attack");
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
    foreach(Collider2D enemy in hitEnemies) {
      enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
    }
  }

  void OnDrawGizmosSelected() {
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
  }
}
