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
  public LayerMask enemyLayers;
  public float _attackTimer = -1.0f;

  // Light Attacks
  [SerializeField] private int _firstLightAttackDamage = 10;
  [SerializeField] private float _firstLightAttackRange = 0.20f;
  [SerializeField] private float _firstLightAttackCooldown = 0.4f;
  public IEnumerator _firstLightAttackRoutine;

  [SerializeField] private int _secondLightAttackDamage = 15;
  [SerializeField] private float _secondLightAttackRange = 0.20f;
  [SerializeField] private float _secondLightAttackCooldown = 0.4f;
  public IEnumerator _secondLightAttackRoutine;

  [SerializeField] private int _thirdLightAttackDamage = 20;
  [SerializeField] private float _thirdLightAttackRange = 0.20f;
  [SerializeField] private float _thirdLightAttackCooldown = 0.4f;
  public IEnumerator _thirdLightAttackRoutine;

  [SerializeField] private int _airLightAttackDamage = 10;
  [SerializeField] private float _airLightAttackRange = 0.2f;
  [SerializeField] private float _airLightAttackCooldown = 0.4f;
  public IEnumerator _airLightAttackRoutine;
  public IEnumerator _airLightAttacksRoutine;

  // Heavy Attacks
  [SerializeField] private int _airHeavyAttackDamage = 10;
  [SerializeField] private float _airHeavyAttackRange = 0.3f;
  [SerializeField] private float _airHeavyAttackCooldown = 0.4f;
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
    }
  }

  [SerializeField] private bool _secondLightAttack;
  public bool SecondLightAttack {
    get { return _secondLightAttack; }
    set {
      if (value == _secondLightAttack) return;
      _secondLightAttack = value;
      _animator.SetBool("secondLightAttack", _secondLightAttack);
    }
  }

  [SerializeField] private bool _thirdLightAttack;
  public bool ThirdLightAttack {
    get { return _thirdLightAttack; }
    set {
      if (value == _thirdLightAttack) return;
      _thirdLightAttack = value;
      _animator.SetBool("thirdLightAttack", _thirdLightAttack);
    }
  }

  [SerializeField] private bool _airHeavyAttack;
  public bool AirHeavyAttack {
    get { return _airHeavyAttack; }
    set {
      if (value == _airHeavyAttack) return;
      _airHeavyAttack = value;
      _animator.SetBool("airHeavyAttack", _airHeavyAttack);
    }
  }

  [SerializeField] private bool _airLightAttack;
  public bool AirLightAttack {
    get { return _airLightAttack; }
    set {
      if (value == _airLightAttack) return;
      _airLightAttack = value;
      _animator.SetBool("airLightAttack", _airLightAttack);
    }
  }

  void Awake() {
    _animator = GetComponent<Animator>();
    _rb = GetComponent<Rigidbody2D>();
    _playerControllerScript = GetComponent<PlayerController>();
    _playerHealthScript = GetComponent<PlayerHealth>();
  }

  public void Attack(string type) {
    if (Time.time < _attackTimer) { return; }
    if (type == "Light") {
      if (_playerControllerScript._isGrounded) {
        _firstLightAttackRoutine = firstLightAttackRoutine();
        StartCoroutine(_firstLightAttackRoutine);
      } else {
        _airLightAttackRoutine = airLightAttackRoutine();
        StartCoroutine(_airLightAttackRoutine);
      }
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

    float duration = 0.0f;
    bool queueSecondLightAttack = false;
    bool queueHeavyAttack = false;
    _attackTimer = 10000000.0f;
    while (duration < 0.25f && !_playerHealthScript.Damaged && !_playerHealthScript.Dying) {
      if (Input.GetButton("Fire1") && duration > 0.1f) {
        queueSecondLightAttack = true;
      } else if (Input.GetButton("Fire2") && duration > 0.1f) {
        queueHeavyAttack = true;
      }
      duration += Time.deltaTime;
      _rb.velocity = new Vector2(0, 0) * 0;
      _rb.gravityScale = 0.0f;
      yield return 0;
    }

    _attackTimer = Time.time + _firstLightAttackCooldown;

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

    float duration = 0.0f;
    bool queueThirdLightAttack = false;
    bool queueHeavyAttack = false;
    _attackTimer = 10000000.0f;
    while (duration < 0.25f && !_playerHealthScript.Damaged && !_playerHealthScript.Dying) {
      if (Input.GetButton("Fire1") && duration > 0.1f) {
        queueThirdLightAttack = true;
      } else if (Input.GetButton("Fire2") && duration > 0.1f) {
        queueHeavyAttack = true;
      }      
      duration += Time.deltaTime;
      _rb.velocity = new Vector2(0, 0) * 0;
      _rb.gravityScale = 0.0f;
      yield return 0;
    }

    _attackTimer = Time.time + _secondLightAttackCooldown;

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

    float duration = 0.0f;
    bool queueHeavyAttack = false;
    _attackTimer = 10000000.0f;
    while (duration < 0.25f && !_playerHealthScript.Damaged && !_playerHealthScript.Dying) {
      if (Input.GetButton("Fire2") && duration > 0.1f) queueHeavyAttack = true;
      duration += Time.deltaTime;
      _rb.velocity = new Vector2(0, 0) * 0;
      _rb.gravityScale = 0.0f;
      yield return 0;
    }

    _attackTimer = Time.time + _thirdLightAttackCooldown;

    ThirdLightAttack = false;

    if (queueHeavyAttack) {
      _airHeavyAttackRoutine = airHeavyAttackRoutine();
      StartCoroutine(_airHeavyAttackRoutine);
    } else {
      Attacking = false;
    }
  }

  public IEnumerator airLightAttackRoutine() {
    Attacking = true;
    AirLightAttack = true;

    float duration = 0.0f;
    bool queueHeavyAttack = false;
    _attackTimer = 10000000.0f;
    while (!_playerControllerScript._isGrounded && !_playerHealthScript.Damaged && !_playerHealthScript.Dying) {
      if (_playerControllerScript.Dashing) { break; }
      Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airLightAttackRange, enemyLayers);
      foreach(Collider2D enemyHitbox in hitEnemies) { enemyHitbox.transform.parent.GetComponent<Enemy>().TakeDamage(_airLightAttackDamage); }
      if (hitEnemies.Length > 0) { _playerControllerScript.Bounce(); }
      yield return new WaitForSeconds(0.01f);
    }

    _attackTimer = Time.time + _airHeavyAttackCooldown;

    AirLightAttack = false;

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

    float duration = 0.0f;
    _attackTimer = 10000000.0f;
    while (duration < 0.5f && !_playerHealthScript.Damaged && !_playerHealthScript.Dying) {
      duration += Time.deltaTime;
      _rb.velocity = new Vector2(0, 0) * 0;
      _rb.gravityScale = 0.0f;
      yield return 0;
    }

    _attackTimer = Time.time + _airHeavyAttackCooldown;

    Attacking = false;
    AirHeavyAttack = false;
  }

  public IEnumerator airHeavyAttacksRoutine() {
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    foreach(Collider2D enemyHitbox in hitEnemies) { enemyHitbox.transform.parent.GetComponent<Enemy>().TakeDamage(_airHeavyAttackDamage); }
    yield return new WaitForSeconds(0.125f);
    hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    foreach(Collider2D enemyHitbox in hitEnemies) { enemyHitbox.transform.parent.GetComponent<Enemy>().TakeDamage(_airHeavyAttackDamage); }
    yield return new WaitForSeconds(0.125f);
    hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    foreach(Collider2D enemyHitbox in hitEnemies) { enemyHitbox.transform.parent.GetComponent<Enemy>().TakeDamage(_airHeavyAttackDamage); }
    yield return new WaitForSeconds(0.125f);
    hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    foreach(Collider2D enemyHitbox in hitEnemies) { enemyHitbox.transform.parent.GetComponent<Enemy>().TakeDamage(_airHeavyAttackDamage); }
    yield return new WaitForSeconds(0.125f);
  }

  void OnDrawGizmosSelected() {
    if (FirstLightAttack) Gizmos.DrawWireSphere(attackPoint.position, _firstLightAttackRange);
    if (SecondLightAttack) Gizmos.DrawWireSphere(attackPoint.position, _secondLightAttackRange);
    if (ThirdLightAttack) Gizmos.DrawWireSphere(attackPoint.position, _thirdLightAttackRange);
    if (AirLightAttack) Gizmos.DrawWireSphere(transform.position, _airLightAttackRange);
    if (AirHeavyAttack) Gizmos.DrawWireSphere(transform.position, _airHeavyAttackRange);
  }

  void OnCollisionStay2D(Collision2D collision) {
    if (HarmfulGround.Contains(collision.gameObject.tag)) {
      _playerControllerScript.ResetAnimationVariables();
      _playerHealthScript.TakeDamage(collision.transform);
    }
  }

  void OnTriggerStay2D(Collider2D collision) {
    if (collision.gameObject.layer == 13) {
      _playerControllerScript.ResetAnimationVariables();
      _playerHealthScript.TakeDamage(collision.transform.parent);
    }
  }
}
