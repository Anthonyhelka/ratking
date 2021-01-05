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
  public LayerMask enemyLayers;
  public float _nextAttackTime = -1.0f;

  // Light Attacks
  [SerializeField] private int _firstLightAttackDamage = 10;
  [SerializeField] private float _firstLightAttackRange = 0.15f;
  [SerializeField] private float _firstLightAttackCooldown = 0.5f;
  public IEnumerator _firstLightAttackRoutine;

  [SerializeField] private int _secondLightAttackDamage = 20;
  [SerializeField] private float _secondLightAttackRange = 0.15f;
  [SerializeField] private float _secondLightAttackCooldown = 0.6f;
  public IEnumerator _secondLightAttackRoutine;

  [SerializeField] private int _thirdLightAttackDamage = 30;
  [SerializeField] private float _thirdLightAttackRange = 0.15f;
  [SerializeField] private float _thirdLightAttackCooldown = 0.7f;
  public IEnumerator _thirdLightAttackRoutine;

  // Heavy Attacks
  [SerializeField] private int _airHeavyAttackDamage = 10;
  [SerializeField] private float _airHeavyAttackRange = 0.25f;
  [SerializeField] private float _airHeavyAttackCooldown = 0.8f;

  public IEnumerator _airHeavyAttackRoutine;

  public List<string> HarmfulGround = new List<string>() { "Spikes" };

  [SerializeField] private bool _attacking;
  public bool Attacking {
    get { return _attacking; }
    set {
      if (value == _attacking) return;
      _attacking = value;
      _animator.SetBool("attacking", _attacking);
    }
  }

  [SerializeField] private bool _firstLightAttack;
  public bool FirstLightAttack {
    get { return _firstLightAttack; }
    set {
      if (value == _firstLightAttack) return;
      _firstLightAttack = value;
      _animator.SetBool("firstLightAttack", _firstLightAttack);
      _attackEffectAnimator.SetBool("firstLightAttack", _firstLightAttack);
    }
  }

  [SerializeField] private bool _secondLightAttack;
  public bool SecondLightAttack {
    get { return _secondLightAttack; }
    set {
      if (value == _secondLightAttack) return;
      _secondLightAttack = value;
      _animator.SetBool("secondLightAttack", _secondLightAttack);
      _attackEffectAnimator.SetBool("secondLightAttack", _secondLightAttack);
    }
  }

  [SerializeField] private bool _thirdLightAttack;
  public bool ThirdLightAttack {
    get { return _thirdLightAttack; }
    set {
      if (value == _thirdLightAttack) return;
      _thirdLightAttack = value;
      _animator.SetBool("thirdLightAttack", _thirdLightAttack);
      _attackEffectAnimator.SetBool("thirdLightAttack", _thirdLightAttack);
    }
  }

  [SerializeField] private bool _airHeavyAttack;
  public bool AirHeavyAttack {
    get { return _airHeavyAttack; }
    set {
      if (value == _airHeavyAttack) return;
      _airHeavyAttack = value;
      _animator.SetBool("airHeavyAttack", _airHeavyAttack);
      _attackEffectAnimator.SetBool("airHeavyAttack", _airHeavyAttack);
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

  public void Attack(string type) {
    if (Time.time < _nextAttackTime) { return; }
    if (type == "Light") {
      _firstLightAttackRoutine = firstLightAttackRoutine();
      StartCoroutine(_firstLightAttackRoutine);
    } else if (type == "Heavy") {
      _airHeavyAttackRoutine = airHeavyAttackRoutine();
      StartCoroutine(_airHeavyAttackRoutine);
    }
  }

  public IEnumerator firstLightAttackRoutine() {
    Attacking = true;
    FirstLightAttack = true;

    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _firstLightAttackRange, enemyLayers);
    foreach(Collider2D enemyHitbox in hitEnemies) { enemyHitbox.transform.parent.GetComponent<Enemy>().TakeDamage(_firstLightAttackDamage); }
    _nextAttackTime = Time.time + _firstLightAttackCooldown;

    float duration = 0.0f;
    bool queueSecondLightAttack = false;
    bool queueHeavyAttack = false;
    while (duration < 0.25f && !_playerHealthScript.Damaged && !_playerHealthScript.Dying) {
      if (Input.GetButtonDown("Fire1") && duration > 0.05f) {
        queueSecondLightAttack = true;
      } else if (Input.GetButtonDown("Fire2") && duration > 0.05f) {
        queueHeavyAttack = true;
      }
      duration += Time.deltaTime;
      _rb.velocity = new Vector2(0, 0) * 0;
      _rb.gravityScale = 0.0f;
      yield return 0;
    }

    FirstLightAttack = false;

    if (queueSecondLightAttack) {
      _secondLightAttackRoutine = secondLightAttackRoutine();
      StartCoroutine(_secondLightAttackRoutine);
    } else if (queueHeavyAttack) {
      _airHeavyAttackRoutine = airHeavyAttackRoutine();
      StartCoroutine(_airHeavyAttackRoutine);
    } else {
      Attacking = false;
    }
  }

  public IEnumerator secondLightAttackRoutine() {
    Attacking = true;
    SecondLightAttack = true;

    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _secondLightAttackRange, enemyLayers);
    foreach(Collider2D enemyHitbox in hitEnemies) { enemyHitbox.transform.parent.GetComponent<Enemy>().TakeDamage(_secondLightAttackDamage); }
    _nextAttackTime = Time.time + _secondLightAttackCooldown;

    float duration = 0.0f;
    bool queueThirdLightAttack = false;
    bool queueHeavyAttack = false;
    while (duration < 0.25f && !_playerHealthScript.Damaged && !_playerHealthScript.Dying) {
      if (Input.GetButtonDown("Fire1") && duration > 0.05f) {
        queueThirdLightAttack = true;
      } else if (Input.GetButtonDown("Fire2") && duration > 0.05f) {
        queueHeavyAttack = true;
      }      
      duration += Time.deltaTime;
      _rb.velocity = new Vector2(0, 0) * 0;
      _rb.gravityScale = 0.0f;
      yield return 0;
    }

    SecondLightAttack = false;
    
    if (queueThirdLightAttack) {
      _thirdLightAttackRoutine = thirdLightAttackRoutine();
      StartCoroutine(_thirdLightAttackRoutine);
    } else if (queueHeavyAttack) {
      _airHeavyAttackRoutine = airHeavyAttackRoutine();
      StartCoroutine(_airHeavyAttackRoutine);
    } else {
      Attacking = false;
    }
  }

  public IEnumerator thirdLightAttackRoutine() {
    Attacking = true;
    ThirdLightAttack = true;

    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _thirdLightAttackRange, enemyLayers);
    foreach(Collider2D enemyHitbox in hitEnemies) { enemyHitbox.transform.parent.GetComponent<Enemy>().TakeDamage(_thirdLightAttackDamage); }
    _nextAttackTime = Time.time + _thirdLightAttackCooldown;

    float duration = 0.0f;
    bool queueHeavyAttack = false;
    while (duration < 0.25f && !_playerHealthScript.Damaged && !_playerHealthScript.Dying) {
      if (Input.GetButtonDown("Fire2") && duration > 0.05f) queueHeavyAttack = true;
      duration += Time.deltaTime;
      _rb.velocity = new Vector2(0, 0) * 0;
      _rb.gravityScale = 0.0f;
      yield return 0;
    }

    ThirdLightAttack = false;

    if (queueHeavyAttack) {
      _airHeavyAttackRoutine = airHeavyAttackRoutine();
      StartCoroutine(_airHeavyAttackRoutine);
    } else {
      Attacking = false;
    }
  }

  public IEnumerator airHeavyAttackRoutine() {
    Attacking = true;
    AirHeavyAttack = true;

    StartCoroutine(airHeavyAttacksRoutine());

    _nextAttackTime = Time.time + _airHeavyAttackCooldown;

    float duration = 0.0f;
    while (duration < 0.4f && !_playerHealthScript.Damaged && !_playerHealthScript.Dying) {
      duration += Time.deltaTime;
      _rb.velocity = new Vector2(0, 0) * 0;
      _rb.gravityScale = 0.0f;
      yield return 0;
    }

    Attacking = false;
    AirHeavyAttack = false;
  }

  public IEnumerator airHeavyAttacksRoutine() {
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    foreach(Collider2D enemyHitbox in hitEnemies) { enemyHitbox.transform.parent.GetComponent<Enemy>().TakeDamage(_airHeavyAttackDamage); }
    yield return new WaitForSeconds(0.1f);
    hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    foreach(Collider2D enemyHitbox in hitEnemies) { enemyHitbox.transform.parent.GetComponent<Enemy>().TakeDamage(_airHeavyAttackDamage); }
    yield return new WaitForSeconds(0.1f);
    hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    foreach(Collider2D enemyHitbox in hitEnemies) { enemyHitbox.transform.parent.GetComponent<Enemy>().TakeDamage(_airHeavyAttackDamage); }
    yield return new WaitForSeconds(0.1f);
    hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    foreach(Collider2D enemyHitbox in hitEnemies) { enemyHitbox.transform.parent.GetComponent<Enemy>().TakeDamage(_airHeavyAttackDamage); }
    yield return new WaitForSeconds(0.1f);
  }
      
  void OnDrawGizmosSelected() {
    if (FirstLightAttack) Gizmos.DrawWireSphere(attackPoint.position, _firstLightAttackRange);
    if (SecondLightAttack) Gizmos.DrawWireSphere(attackPoint.position, _secondLightAttackRange);
    if (ThirdLightAttack) Gizmos.DrawWireSphere(attackPoint.position, _thirdLightAttackRange);
    if (AirHeavyAttack) Gizmos.DrawWireSphere(transform.position, _airHeavyAttackRange);
  }

  void OnCollisionStay2D(Collision2D collision) {
    if (HarmfulGround.Contains(collision.gameObject.tag)) {
      _playerControllerScript.ResetAnimationVariables();
      _playerHealthScript.TakeDamage(collision.transform.tag);
    }
  }

  void OnTriggerStay2D(Collider2D collision) {
    if (collision.gameObject.layer == 13) {
      _playerControllerScript.ResetAnimationVariables();
      _playerHealthScript.TakeDamage(collision.transform.parent.tag);
    }
  }
}
