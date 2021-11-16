using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour {
  public Transform player;

  public SpriteRenderer sr;
  public SpriteRenderer playerSr;

  private Color color;

  [SerializeField] private float activeTime;
  private float timeActivated;
  private float alpha;
  [SerializeField] private float alphaSet = 0.8f;
  [SerializeField] private float alphaMultiplier = 0.85f;

  private void OnEnable() {
    sr = GetComponent<SpriteRenderer>();
    player = GameObject.FindGameObjectWithTag("Player").transform;
    playerSr = player.GetComponent<SpriteRenderer>();

    alpha = alphaSet;
    sr.sprite = playerSr.sprite;
    transform.position = player.position;
    transform.rotation = player.rotation;
    timeActivated = Time.time;
  }

  private void FixedUpdate() {
    alpha *= alphaMultiplier;
    color = new Color(1.0f, 1.0f, 1.0f, alpha);
    sr.color = color;

    if (Time.time >= timeActivated + activeTime) {
      PlayerAfterImagePool.Instance.AddToPool(gameObject);
    }
  }
}
