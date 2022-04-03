using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {
  private Animator _animator;
  private Rigidbody2D _rb;
  private PlayerHealth _playerHealthScript;
  private PlayerController _playerControllerScript;

  public Transform attackPoint;
  public Transform bouncePoint;
  public LayerMask enemyLayers;
  public float _attackTimer = -1.0f;
  private AttackDetails attackDetails;

  // Light Attacks
  [SerializeField] private int _firstLightAttackDamage = 30;
  [SerializeField] private float _firstLightAttackRange = 0.20f;
  [SerializeField] private float _firstLightAttackCooldown = 0.2f;
  public IEnumerator _firstLightAttackRoutine;

  [SerializeField] private int _secondLightAttackDamage = 30;
  [SerializeField] private float _secondLightAttackRange = 0.20f;
  [SerializeField] private float _secondLightAttackCooldown = 0.2f;
  public IEnumerator _secondLightAttackRoutine;

  [SerializeField] private int _airLightAttackDamage = 10;
  [SerializeField] private float _airLightAttackRange = 0.2f;
  [SerializeField] private float _airLightAttackCooldown = 0.2f;
  public IEnumerator _airLightAttackRoutine;
  public IEnumerator _airLightAttacksRoutine;

  // Heavy Attacks
  [SerializeField] private int _airHeavyAttackDamage = 15;
  [SerializeField] private float _airHeavyAttackRange = 0.3f;
  [SerializeField] private float _airHeavyAttackCooldown = 0.2f;
  public IEnumerator _airHeavyAttackRoutine;

  private int lastAttack = 2;

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
      if (!_playerControllerScript._isGrounded) {
        _airLightAttackRoutine = airLightAttackRoutine();
        StartCoroutine(_airLightAttackRoutine);
      } else {
        if (lastAttack == 2) {
          _firstLightAttackRoutine = firstLightAttackRoutine();
          StartCoroutine(_firstLightAttackRoutine);          
          lastAttack = 1;
        } else {
          _secondLightAttackRoutine = secondLightAttackRoutine();
          StartCoroutine(_secondLightAttackRoutine);          
          lastAttack = 2;
        }
      }
    } else if (type == "Heavy") {
      _airHeavyAttackRoutine = airHeavyAttackRoutine();
      StartCoroutine(_airHeavyAttackRoutine);
    }
  }

  public IEnumerator firstLightAttackRoutine() {
    Attacking = true;
    FirstLightAttack = true;

    attackDetails.damageAmount = _firstLightAttackDamage;
    attackDetails.position = transform.position;
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _firstLightAttackRange, enemyLayers);
    foreach(Collider2D enemy in hitEnemies) { enemy.transform.parent.SendMessage("Damage", attackDetails); }

    float duration = 0.0f;
    bool queueHeavyAttack = false;
    _attackTimer = 10000000.0f;
    while (duration < 0.3f && !_playerControllerScript.Knockback && !_playerHealthScript.Dying) {
      if (Input.GetButtonDown("Fire1") && duration > 0.1f) {
      } else if (Input.GetButtonDown("Fire2") && duration > 0.1f) {
        queueHeavyAttack = true;
      }
      duration += Time.deltaTime;
      yield return 0;
    }

    _attackTimer = Time.time + _firstLightAttackCooldown;

    FirstLightAttack = false;

    if (queueHeavyAttack) {
      _airHeavyAttackRoutine = airHeavyAttackRoutine();
      StartCoroutine(_airHeavyAttackRoutine);
    } else {
      Attacking = false;
    }
  }

  public IEnumerator secondLightAttackRoutine() {
    Attacking = true;
    SecondLightAttack = true;

    attackDetails.damageAmount = _secondLightAttackDamage;
    attackDetails.position = transform.position;
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _firstLightAttackRange, enemyLayers);
    foreach(Collider2D enemy in hitEnemies) { enemy.transform.parent.SendMessage("Damage", attackDetails); }

    float duration = 0.0f;
    bool queueHeavyAttack = false;
    _attackTimer = 10000000.0f;
    while (duration < 0.3f && !_playerControllerScript.Knockback && !_playerHealthScript.Dying) {
      if (Input.GetButtonDown("Fire2") && duration > 0.1f) {
        queueHeavyAttack = true;
      }      
      duration += Time.deltaTime;
      yield return 0;
    }

    _attackTimer = Time.time + _secondLightAttackCooldown;

    SecondLightAttack = false;
    
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

    bool queueHeavyAttack = false;
    _attackTimer = 10000000.0f;
    while (!queueHeavyAttack && !_playerControllerScript.Knockback && !_playerHealthScript.Dying) {
      if (_playerControllerScript._isGrounded) {
        _playerControllerScript.Roll();
        break;
      } else if (Input.GetButtonDown("Fire2")) {
        queueHeavyAttack = true;
        break;
      } else if (_playerControllerScript.Dashing) { 
        break; 
      }

      Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(bouncePoint.position, _airLightAttackRange, enemyLayers);
      attackDetails.damageAmount = _airLightAttackDamage;
      attackDetails.position = transform.position;
      foreach(Collider2D enemy in hitEnemies) { enemy.transform.parent.SendMessage("Damage", attackDetails); }

      if (hitEnemies.Length > 0) { 
        _playerControllerScript.Bounce(); 
        break;
      }

      yield return new WaitForSeconds(0.01f);
    }

    _attackTimer = Time.time + _airLightAttackCooldown;

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
    while (duration < .75f && !_playerControllerScript.Knockback && !_playerHealthScript.Dying) {
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
    attackDetails.damageAmount = _airLightAttackDamage;
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    attackDetails.position = transform.position;
    foreach(Collider2D enemy in hitEnemies) { enemy.transform.parent.SendMessage("Damage", attackDetails); }
    yield return new WaitForSeconds(0.1f);
    hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    attackDetails.position = transform.position;
    foreach(Collider2D enemy in hitEnemies) { enemy.transform.parent.SendMessage("Damage", attackDetails); }
    yield return new WaitForSeconds(0.1f);
    hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    attackDetails.position = transform.position;
    foreach(Collider2D enemy in hitEnemies) { enemy.transform.parent.SendMessage("Damage", attackDetails); }
    yield return new WaitForSeconds(0.1f);
    hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    attackDetails.position = transform.position;
    foreach(Collider2D enemy in hitEnemies) { enemy.transform.parent.SendMessage("Damage", attackDetails); }
    yield return new WaitForSeconds(0.1f);
    hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    attackDetails.position = transform.position;
    foreach(Collider2D enemy in hitEnemies) { enemy.transform.parent.SendMessage("Damage", attackDetails); }
    yield return new WaitForSeconds(0.1f);
    hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    attackDetails.position = transform.position;
    foreach(Collider2D enemy in hitEnemies) { enemy.transform.parent.SendMessage("Damage", attackDetails); }
    yield return new WaitForSeconds(0.1f);
    hitEnemies = Physics2D.OverlapCircleAll(transform.position, _airHeavyAttackRange, enemyLayers);
    attackDetails.position = transform.position;
    foreach(Collider2D enemy in hitEnemies) { enemy.transform.parent.SendMessage("Damage", attackDetails); }
  }

  void OnDrawGizmosSelected() {
    if (FirstLightAttack) Gizmos.DrawWireSphere(attackPoint.position, _firstLightAttackRange);
    if (SecondLightAttack) Gizmos.DrawWireSphere(attackPoint.position, _secondLightAttackRange);
    if (AirLightAttack) Gizmos.DrawWireSphere(bouncePoint.position, _airLightAttackRange);
    if (AirHeavyAttack) Gizmos.DrawWireSphere(transform.position, _airHeavyAttackRange);
  }
}
