using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  [SerializeField] private Slider _healthBarSlider;
  [SerializeField] private Color _healthBarLowColor;
  [SerializeField] private Color _healthBarHighColor;
  [SerializeField] private Vector3 _healthBarOffset;

  void Update() {
    _healthBarSlider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + _healthBarOffset);
  }

  public void SetHealth(float currentHealth, float maxHealth) {
    _healthBarSlider.gameObject.SetActive(currentHealth < maxHealth);
    _healthBarSlider.value = currentHealth;
    _healthBarSlider.maxValue = maxHealth;
    _healthBarSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(_healthBarLowColor, _healthBarHighColor, _healthBarSlider.normalizedValue);
  }

  public void Destroy() {
    Destroy(gameObject);
  }
}
