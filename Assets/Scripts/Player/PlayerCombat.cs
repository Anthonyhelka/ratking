using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
  private Animator _animator;
  private Rigidbody2D _rb;
  private PlayerHealth _playerHealthScript;
  private PlayerController _playerControllerScript;

  public Transform attackPoint;
  public float attackRange = 0.1f;
  public LayerMask enemyLayers;
  public int attackDamage = 1;
  public float attackCooldown = 1.0f;
  private float _nextAttackTime = -1.0f;
  public List<string> Enemies = new List<string>() { "Infected", "Spikes" };

  [SerializeField] private bool _attacking;
  public bool Attacking {
    get { return _attacking; }
    set {
      if (value == _attacking) return;
      _attacking = value;
      _animator.SetBool("attacking", _attacking);
    }
  }

  void Awake()
  {
    _animator = GetComponent<Animator>();
    _rb = GetComponent<Rigidbody2D>();
    _playerControllerScript = GetComponent<PlayerController>();
    _playerHealthScript = GetComponent<PlayerHealth>();
  }

  public void Attack() {
    if (Time.time < _nextAttackTime) { return; }
    StartCoroutine(SwingRoutine());
  }

  IEnumerator SwingRoutine() {
    Attacking = true;
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
    foreach(Collider2D enemy in hitEnemies) { enemy.GetComponent<Enemy>().TakeDamage(attackDamage); }
    _nextAttackTime = Time.time + attackCooldown;
    float duration = 0.0f;
    while (duration < 0.25f) {
      duration += Time.deltaTime;
      _playerControllerScript._lockPlayerInput = true; 
      _rb.velocity = new Vector2(0, 0) * 0;
      _rb.gravityScale = 0.0f;
      yield return 0;
    }
    _playerControllerScript._lockPlayerInput = false; 
    Attacking = false;
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
