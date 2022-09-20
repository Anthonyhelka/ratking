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
  [SerializeField] private Vector2 knockbackAngle = new Vector2(1.0f, 0.0f);
  [SerializeField] private float knockbackStrength = 1.0f;
  
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

    Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(hitboxPosition.position, hitboxRadius, whatIsEnemy);
    foreach (Collider2D collider in detectedObjects) {
      IDamageable damageable = collider.GetComponentInParent<IDamageable>();
      if (damageable != null) {
        attackDetails.position = transform.position;
        damageable.Damage(attackDetails);
      }

      IKnockbackable knockbackable = collider.GetComponentInParent<IKnockbackable>();
      if (knockbackable != null) {
        knockbackable.Knockback(knockbackAngle, knockbackStrength, collider.transform.position.x <= transform.position.x ? 1 : -1);
      }
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
