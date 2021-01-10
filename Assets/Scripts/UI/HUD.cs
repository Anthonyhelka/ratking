using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
  private PlayerController _playerControllerScript;
  private PlayerHealth _playerHealthScript;
  private PlayerCombat _playerCombatScript;
  private Animator _portraitAnimator;
  private Slider _dashSlider;
  private Slider _attackSlider;

  private float _health;
  [SerializeField] private int _heartCount;
  [SerializeField] private Image[] _hearts;
  [SerializeField] private Sprite _fullHeart;
  [SerializeField] private Sprite _emptyHeart;

  [SerializeField] public bool _healthy;
  public bool Healthy {
    get { return _healthy; }
    set {
      if (value == _healthy) return;
      _healthy = value;
      _portraitAnimator.SetBool("healthy", _healthy);
    }
  }

  [SerializeField] public bool _injured;
  public bool Injured {
    get { return _injured; }
    set {
      if (value == _injured) return;
      _injured = value;
      _portraitAnimator.SetBool("injured", _injured);
    }
  }

  [SerializeField] public bool _wounded;
  public bool Wounded {
    get { return _wounded; }
    set {
      if (value == _wounded) return;
      _wounded = value;
      _portraitAnimator.SetBool("wounded", _wounded);
    }
  }

  [SerializeField] public bool _dead;
  public bool Dead {
    get { return _dead; }
    set {
      if (value == _dead) return;
      _dead = value;
      _portraitAnimator.SetBool("dead", _dead);
    }
  }

  void Awake() {
    _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    _playerHealthScript = GameObject.Find("Player").GetComponent<PlayerHealth>();
    _playerCombatScript = GameObject.Find("Player").GetComponent<PlayerCombat>();
    _portraitAnimator = GameObject.Find("Portrait").GetComponent<Animator>();
    _dashSlider = GameObject.Find("Dash_Slider").GetComponent<Slider>();
    _attackSlider = GameObject.Find("Attack_Slider").GetComponent<Slider>();
  }

  void Update() {
    _health = _playerHealthScript.health;
    CalculateHearts();
    CalculatePortrait();
    CalculateDashSlider();
    CalculateAttackSlider();
  }

  void CalculateHearts() {
    if (_health > _heartCount) {
      _health = _heartCount;
    }
    for (int i = 0; i < _hearts.Length; i++) {
      if (i < _health) {
        _hearts[i].sprite = _fullHeart;
      } else {
        _hearts[i].sprite = _emptyHeart;
      }

      if (i < _heartCount) {
        _hearts[i].enabled = true;
      } else {
        _hearts[i].enabled = false;
      }
    }
  }

  void CalculatePortrait() {
    Healthy = false;
    Injured = false;
    Wounded = false;
    Dead = false;

    if (_health == _heartCount) {
      Healthy = true;
    } else if (_health == 2) {
      Injured = true;
    } else if (_health == 1) {
      Wounded = true;
    } else if (_health <= 0) {
      Dead = true;
    }
  }

  void CalculateDashSlider() {
    float dashProgress = _playerControllerScript._dashCooldown - (_playerControllerScript._dashTimer - Time.time) * 2 + _playerControllerScript._dashTimer - Time.time;
    _dashSlider.maxValue = _playerControllerScript._dashCooldown;
    _dashSlider.value = dashProgress;
    if (dashProgress >= _playerControllerScript._dashCooldown && _playerControllerScript._dashCount == 0) {
      _dashSlider.fillRect.GetComponentInChildren<Image>().color = new Color(0.0f, 0.55f, 1.0f);
    } else {
      _dashSlider.fillRect.GetComponentInChildren<Image>().color = new Color(0.65f, 0.65f, 0.65f); 
    }
  }

  void CalculateAttackSlider() {
    float attackProgress = 0.4f - (_playerCombatScript._attackTimer - Time.time) * 2 + _playerCombatScript._attackTimer - Time.time;
    _attackSlider.maxValue = 0.4f;
    _attackSlider.value = attackProgress;
    if (attackProgress >= 0.4f) {
      _attackSlider.fillRect.GetComponentInChildren<Image>().color = new Color(1.0f, 0.1f, 0.0f);
    } else {
      _attackSlider.fillRect.GetComponentInChildren<Image>().color = new Color(0.65f, 0.65f, 0.65f); 
    }
  }
}
