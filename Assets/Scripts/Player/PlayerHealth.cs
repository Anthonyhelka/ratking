using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
  private Animator _animator;
  private Rigidbody2D _rb;
  private PlayerController _playerControllerScript;
  private PlayerCombat _playerCombatScript;
  private GameOverMenu _gameOverMenuScript;

  public int health;
  private int _maxHealth;

  // Invincibility
  [SerializeField] private float  _invincibilityCooldown = 1.5f;
  [SerializeField] private float _invincibilityTimer = -1.0f;
  private IEnumerator _invincibilityRoutine;

  private float _vomitDuration = 3.4f;
  private float _spikesDuration = 1.9f;
  private float _knightDuration = 2.5f;
  private float _strikeDuration = 1.9f;

  [SerializeField] public bool _dying;
  public bool Dying {
    get { return _dying; }
    set {
      if (value == _dying) return;
      _dying = value;
      _animator.SetBool("dying", _dying);
    }
  }

  [SerializeField] public bool _vomit;
  public bool Vomit {
    get { return _vomit; }
    set {
      if (value == _vomit) return;
      _vomit = value;
      _animator.SetBool("vomit", _vomit);
    }
  }

  [SerializeField] public bool _spikes;
  public bool Spikes {
    get { return _spikes; }
    set {
      if (value == _spikes) return;
      _spikes = value;
      _animator.SetBool("spikes", _spikes);
    }
  }

  [SerializeField] public bool _knight;
  public bool Knight {
    get { return _knight; }
    set {
      if (value == _knight) return;
      _knight = value;
      _animator.SetBool("knight", _knight);
    }
  }

  [SerializeField] public bool _strike;
  public bool Strike {
    get { return _strike; }
    set {
      if (value == _strike) return;
      _strike = value;
      _animator.SetBool("strike", _strike);
    }
  }

  void Awake() {
    _rb = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();
    _playerControllerScript = GetComponent<PlayerController>();
    _playerCombatScript = GetComponent<PlayerCombat>();
    _gameOverMenuScript = GameObject.Find("UI").GetComponent<GameOverMenu>();
    _maxHealth = health;
  }

  void Damage(AttackDetails attackDetails) {
    if (Time.time < _invincibilityTimer || Dying) return;

    health -= attackDetails.damageAmount;

    int direction;
    if (attackDetails.position.x < transform.position.x) {
      direction = 1;
    } else {
      direction = -1;
    }

    if (health <= 0) {
      _playerControllerScript.doKnockback(direction);
      StartCoroutine(PlayerDeathRoutine(attackDetails.type));
    } else if (health > 0) {
      _playerControllerScript.doKnockback(direction);
      _invincibilityRoutine = InvincibilityRoutine();
      StartCoroutine(_invincibilityRoutine);
    }
  }

  void Heal(int amount) {
    health += amount;

    if (health > _maxHealth) {
      health = _maxHealth;
    }
  }

  // IEnumerator DamagedPlayerRoutine(Transform enemy) {
  //   _invincibilityTimer = Time.time + _invincibilityCooldown;

  //   Vector2 direction = (transform.position - enemy.position);
  //   direction = direction.normalized;
  //   direction = new Vector2(direction.x, 1.0f);
  //   _rb.velocity = new Vector2(0, 0) * 0;
  //   _rb.gravityScale = 1.0f;

  //   float duration = 0.0f;
  //   Knockback = true;
  //   while (duration < _knockbackDuration && !Dying) {
  //     duration += Time.deltaTime;
  //     if (direction.x > 0.1f && _playerControllerScript._facingRight) _playerControllerScript.Flip();
  //     if (direction.x < 0.1f && !_playerControllerScript._facingRight) _playerControllerScript.Flip();
  //     _rb.AddForce(direction * _knockbackForce * Time.deltaTime, ForceMode2D.Impulse);
  //     yield return 0;
  //   }

  //   Knockback = false;

  //   if (health <= 0) {
  //     StartCoroutine(PlayerDeathRoutine(enemy));
  //   } else if (health > 0) {
  //     _invincibilityRoutine = InvincibilityRoutine();
  //     StartCoroutine(_invincibilityRoutine);
  //   }
  // }

  IEnumerator PlayerDeathRoutine(string tag) {
    Dying = true;
    
    if (_playerControllerScript._dashRoutine != null) StopCoroutine(_playerControllerScript._dashRoutine);
    if (_playerCombatScript._firstLightAttackRoutine != null) StopCoroutine(_playerCombatScript._firstLightAttackRoutine);
    if (_playerCombatScript._secondLightAttackRoutine != null) StopCoroutine(_playerCombatScript._secondLightAttackRoutine);
    if (_playerCombatScript._airHeavyAttackRoutine != null) StopCoroutine(_playerCombatScript._airHeavyAttackRoutine);

    float maxDuration = 0.0f;
    if (tag == "Infected") {
      maxDuration = _vomitDuration;
      Vomit = true;
    } else if (tag == "Spikes") {
      maxDuration = _spikesDuration;
      Spikes = true;
    } else if (tag == "Knight") {
      maxDuration = _knightDuration;
      Knight = true;
    } else if (tag == "Strike") {
      maxDuration = _spikesDuration;
      Strike = true;
    }

    float duration = 0.0f;
    while (duration < maxDuration) {
      duration += Time.deltaTime;
      yield return 0;
    }

    _gameOverMenuScript.GameOver();
  }

  IEnumerator InvincibilityRoutine() {
    _invincibilityTimer = Time.time + _invincibilityCooldown;
    while (Time.time < _invincibilityTimer && !Dying) {
      yield return new WaitForSeconds(0.25f);
      GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
      yield return new WaitForSeconds(0.25f);
      GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
  }
}
