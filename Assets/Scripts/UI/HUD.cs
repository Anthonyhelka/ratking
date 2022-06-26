using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
  private PlayerController _playerControllerScript;
  private Player _playerHealthScript;
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
    _playerHealthScript = GameObject.Find("Player").GetComponent<Player>();
    _portraitAnimator = GameObject.Find("Portrait").GetComponent<Animator>();
  }

  void Update() {
    _health = _playerHealthScript.health;
    _heartCount = _playerHealthScript.maxHealth;
    CalculateHearts();
    CalculatePortrait();
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
}
