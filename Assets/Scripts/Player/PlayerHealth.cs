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

  [SerializeField] private float _knockbackDuration = 0.2f;
  [SerializeField] private float _knockbackForce = 10.0f;
  [SerializeField] private float  _invincibilityCooldown = 2.0f;
  [SerializeField] private float _invincibilityTimer = -1.0f;
  private IEnumerator _invincibilityRoutine;

  private float _vomitDuration = 3.4f;
  private float _spikesDuration = 1.9f;

  [SerializeField] public bool _damaged;
  public bool Damaged {
    get { return _damaged; }
    set {
      if (value == _damaged) return;
      _damaged = value;
      _animator.SetBool("damaged", _damaged);
    }
  }

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

  void Awake() {
    _rb = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();
    _playerControllerScript = GetComponent<PlayerController>();
    _playerCombatScript = GetComponent<PlayerCombat>();
    _gameOverMenuScript = GameObject.Find("UI").GetComponent<GameOverMenu>();
  }

  public void TakeDamage(Transform enemy) {
    if (Time.time < _invincibilityTimer) return;
    health--;
    StartCoroutine(DamagedPlayerRoutine(enemy));
  }

  IEnumerator DamagedPlayerRoutine(Transform enemy) {
    Damaged = true;
    _invincibilityTimer = Time.time + _invincibilityCooldown;

    Vector2 direction = (transform.position - enemy.position);
    direction = direction.normalized;
    _rb.velocity = new Vector2(0, 0) * 0;
    _rb.gravityScale = 1.0f;

    float duration = 0.0f;
    while (duration < _knockbackDuration && !Dying) {
      duration += Time.deltaTime;
      if (direction.x > 0.1f && _playerControllerScript._facingRight) _playerControllerScript.Flip();
      if (direction.x < 0.1f && !_playerControllerScript._facingRight) _playerControllerScript.Flip();
      _rb.AddForce(direction * _knockbackForce * Time.deltaTime, ForceMode2D.Impulse);
      yield return 0;
    }

    Damaged = false;

    if (health <= 0) {
      StartCoroutine(PlayerDeathRoutine(enemy));
    } else if (health > 0) {
      _invincibilityRoutine = InvincibilityRoutine();
      StartCoroutine(_invincibilityRoutine);
    }
  }

  IEnumerator PlayerDeathRoutine(Transform enemy) {
    Dying = true;

    if (_playerControllerScript._dashRoutine != null) StopCoroutine(_playerControllerScript._dashRoutine);
    if (_playerCombatScript._firstLightAttackRoutine != null) StopCoroutine(_playerCombatScript._firstLightAttackRoutine);
    if (_playerCombatScript._secondLightAttackRoutine != null) StopCoroutine(_playerCombatScript._secondLightAttackRoutine);
    if (_playerCombatScript._thirdLightAttackRoutine != null) StopCoroutine(_playerCombatScript._thirdLightAttackRoutine);
    if (_playerCombatScript._airHeavyAttackRoutine != null) StopCoroutine(_playerCombatScript._airHeavyAttackRoutine);

    float maxDuration = 0.0f;
    if (enemy.tag == "Infected") {
      maxDuration = _vomitDuration;
      Vomit = true;
    }
    if (enemy.tag == "Spikes") {
      maxDuration = _spikesDuration;
      Spikes = true;
    }

    float duration = 0.0f;
    while (duration < maxDuration) {
      duration += Time.deltaTime;
      yield return 0;
    }

    _gameOverMenuScript.GameOver();
  }

  IEnumerator InvincibilityRoutine() {
    while (Time.time < _invincibilityTimer && !Dying) {
      yield return new WaitForSeconds(0.25f);
      GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
      yield return new WaitForSeconds(0.25f);
      GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
  }
}
