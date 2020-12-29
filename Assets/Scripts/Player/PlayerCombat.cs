using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
  private Animator _animator;
  private PlayerHealth _playerHealthScript;
  private PlayerController _playerControllerScript;

  public Transform attackPoint;
  public float attackRange = 0.1f;
  public LayerMask enemyLayers;
  public int attackDamage = 1;
  public float attackCooldown = 1.0f;
  private float _nextAttackTime = -1.0f;
  public List<string> Enemies = new List<string>() { "Infected", "Spikes" };

  void Awake()
  {
    _animator = GetComponent<Animator>();
    _playerControllerScript = GetComponent<PlayerController>();
    _playerHealthScript = GetComponent<PlayerHealth>();
  }

  public void Attack() {
    if (Time.time < _nextAttackTime) { return; }
    Debug.Log("Attack");
    _animator.SetTrigger("attack");
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
    foreach(Collider2D enemy in hitEnemies) {
      enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
    }
    _nextAttackTime = Time.time + attackCooldown;
  }

  void OnDrawGizmosSelected() {
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
  }

  void OnCollisionStay2D(Collision2D collision) {
    if (Enemies.Contains(collision.gameObject.tag)) {
      _playerControllerScript.ResetAnimationVariables();
      _playerHealthScript.TakeDamage(collision.transform.tag);
    }
  }
}
