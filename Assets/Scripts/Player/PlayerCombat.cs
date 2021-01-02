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
  private Animator _attackEffectAnimator;
  public float attackRange = 0.15f;
  public LayerMask enemyLayers;
  [SerializeField] private int _firstAttackDamage = 40;
  [SerializeField] private int _secondAttackDamage = 60;
  [SerializeField] private int _thirdAttackDamage = 100;
  [SerializeField] private float _firstAttackCooldown = 0.5f;
  [SerializeField] private float _secondAttackCooldown = 0.75f;
  [SerializeField] private float _thirdAttackCooldown = 1.0f;
  public float _nextAttackTime = -1.0f;
  public IEnumerator _firstAttackRoutine;
  public IEnumerator _secondAttackRoutine;
  public IEnumerator _thirdAttackRoutine;
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

  [SerializeField] private bool _firstAttack;
  public bool FirstAttack {
    get { return _firstAttack; }
    set {
      if (value == _firstAttack) return;
      _firstAttack = value;
      _animator.SetBool("firstAttack", _firstAttack);
      _attackEffectAnimator.SetBool("firstAttack", _firstAttack);
    }
  }

  [SerializeField] private bool _secondAttack;
  public bool SecondAttack {
    get { return _secondAttack; }
    set {
      if (value == _secondAttack) return;
      _secondAttack = value;
      _animator.SetBool("secondAttack", _secondAttack);
      _attackEffectAnimator.SetBool("secondAttack", _secondAttack);
    }
  }

  [SerializeField] private bool _thirdAttack;
  public bool ThirdAttack {
    get { return _thirdAttack; }
    set {
      if (value == _thirdAttack) return;
      _thirdAttack = value;
      _animator.SetBool("thirdAttack", _thirdAttack);
      _attackEffectAnimator.SetBool("thirdAttack", _thirdAttack);
    }
  }


  void Awake()
  {
    _animator = GetComponent<Animator>();
    _rb = GetComponent<Rigidbody2D>();
    _playerControllerScript = GetComponent<PlayerController>();
    _playerHealthScript = GetComponent<PlayerHealth>();
    _attackEffectAnimator = GameObject.Find("Attack_Effect").GetComponent<Animator>();
  }

  public void Attack() {
    if (Time.time < _nextAttackTime) { return; }
    // if (!_playerControllerScript._isGrounded) {
    //   Debug.Log("Spin Attack!");
    // } else {
      _firstAttackRoutine = FirstAttackRoutine();
      StartCoroutine(_firstAttackRoutine);
    // }
  }

  public IEnumerator FirstAttackRoutine() {
    Attacking = true;
    FirstAttack = true;

    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
    foreach(Collider2D enemy in hitEnemies) { enemy.GetComponent<Enemy>().TakeDamage(_firstAttackDamage); }
    _nextAttackTime = Time.time + _firstAttackCooldown;

    float duration = 0.0f;
    bool queueSecondAttack = false;
    while (duration < 0.25f && !_playerHealthScript.Damaged && !_playerHealthScript.Dying) {
      if (Input.GetKeyDown(KeyCode.Mouse0) && duration > 0.05f) queueSecondAttack = true;
      duration += Time.deltaTime;
      _rb.velocity = new Vector2(0, 0) * 0;
      _rb.gravityScale = 0.0f;
      yield return 0;
    }

    FirstAttack = false;

    if (queueSecondAttack) {
      _secondAttackRoutine = SecondAttackRoutine();
      StartCoroutine(_secondAttackRoutine);
    } else {
      Attacking = false;
    }
  }

  public IEnumerator SecondAttackRoutine() {
    Attacking = true;
    SecondAttack = true;

    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
    foreach(Collider2D enemy in hitEnemies) { enemy.GetComponent<Enemy>().TakeDamage(_secondAttackDamage); }
    _nextAttackTime = Time.time + _secondAttackCooldown;

    float duration = 0.0f;
    bool queueThirdAttack = false;
    while (duration < 0.25f && !_playerHealthScript.Damaged && !_playerHealthScript.Dying) {
      if (Input.GetKeyDown(KeyCode.Mouse0) && duration > 0.05f) queueThirdAttack = true;
      duration += Time.deltaTime;
      _rb.velocity = new Vector2(0, 0) * 0;
      _rb.gravityScale = 0.0f;
      yield return 0;
    }

    SecondAttack = false;
    
    if (queueThirdAttack) {
      _thirdAttackRoutine = ThirdAttackRoutine();
      StartCoroutine(_thirdAttackRoutine);
    } else {
      Attacking = false;
    }
  }

  public IEnumerator ThirdAttackRoutine() {
    Attacking = true;
    ThirdAttack = true;

    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
    foreach(Collider2D enemy in hitEnemies) { enemy.GetComponent<Enemy>().TakeDamage(_thirdAttackDamage); }
    _nextAttackTime = Time.time + _thirdAttackCooldown;

    float duration = 0.0f;
    while (duration < 0.25f && !_playerHealthScript.Damaged && !_playerHealthScript.Dying) {
      duration += Time.deltaTime;
      _rb.velocity = new Vector2(0, 0) * 0;
      _rb.gravityScale = 0.0f;
      yield return 0;
    }

    Attacking = false;
    ThirdAttack = false;
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
