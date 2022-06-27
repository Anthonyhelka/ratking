using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour {
  private Rigidbody2D rb;
  public GameObject player;
  private AttackDetails attackDetails;
  private bool goingOut;
  private float speed;
  private float travelDistance;
  private float startPositionX;
  [SerializeField] private float hitboxRadius;
  [SerializeField] private Transform hitboxPosition;
  [SerializeField] private LayerMask whatIsPlayer;
  [SerializeField] private LayerMask whatIsEnemy;
  [SerializeField] private LayerMask whatIsGround;

  private void Start() {
    rb = GetComponent<Rigidbody2D>();
    player = GameObject.Find("Player");
    rb.velocity = transform.right * speed;
    startPositionX = transform.position.x;
    goingOut = true;
  }

  private void Update() {
    attackDetails.position = transform.position;
  }

  private void FixedUpdate() {
    Collider2D playerHit = Physics2D.OverlapCircle(hitboxPosition.position, hitboxRadius, whatIsPlayer);
    Collider2D enemyHit = Physics2D.OverlapCircle(hitboxPosition.position, hitboxRadius, whatIsEnemy);
    Collider2D groundHit = Physics2D.OverlapCircle(hitboxPosition.position, hitboxRadius, whatIsGround);

    if (groundHit && goingOut) {
      goingOut = false;
      transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    if (enemyHit) {
      enemyHit.transform.parent.SendMessage("Damage", attackDetails);
    }

    if (Mathf.Abs(startPositionX - transform.position.x) >= travelDistance && goingOut) {
      goingOut = false;
      transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    if (goingOut) {
      rb.velocity = transform.right * speed;
    } else {
      Vector2 direction = (player.transform.position - transform.position).normalized;
      rb.velocity = direction * speed * 1.25f;
    }

    if (!goingOut && playerHit) {
      player.GetComponent<Player>().BoomerangThrowState.CaughtBoomerang();
      Destroy(gameObject);
    }
  }

  public void FireBoomerang(float speed, float travelDistance, int damage) {
    this.speed = speed;
    this.travelDistance = travelDistance;
    attackDetails.damageAmount = damage;
  }

  private void OnDrawGizmos() {
    Gizmos.DrawWireSphere(hitboxPosition.position, hitboxRadius);
  }
}
