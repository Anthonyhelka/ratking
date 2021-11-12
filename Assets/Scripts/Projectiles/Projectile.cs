using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
  private Rigidbody2D rb;
  private Animator animator;

  private AttackDetails attackDetails;
  private float speed;
  private float travelDistance;
  private float startPositionX;
  [SerializeField] private float gravity = 1.0f;
  [SerializeField] private float hitboxRadius;
  [SerializeField] private bool isGravityOn;
  [SerializeField] private bool hasHitGround;
  [SerializeField] private bool beingDestroyed;
  [SerializeField] private LayerMask whatIsPlayer;
  [SerializeField] private LayerMask whatIsGround;
  [SerializeField] private Transform hitboxPosition;

  private void Start() {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();

    isGravityOn = false;
    rb.gravityScale = 0.0f;
    rb.velocity = transform.right * speed;
    startPositionX = transform.position.x;
  }

  private void Update() {
    if (!hasHitGround) {
      attackDetails.position = transform.position;
      if (isGravityOn) {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
      }
    }
  }

  private void FixedUpdate() {
    if (!hasHitGround && !beingDestroyed) {
      Collider2D playerHit = Physics2D.OverlapCircle(hitboxPosition.position, hitboxRadius, whatIsPlayer);
      Collider2D groundHit = Physics2D.OverlapCircle(hitboxPosition.position, hitboxRadius, whatIsGround);

      if (playerHit) {
        playerHit.transform.SendMessage("Damage", attackDetails);
        DestroyProjectile();
      } else if (groundHit) {
        hasHitGround = true;  
        DestroyProjectile();
      }
      if (Mathf.Abs(startPositionX - transform.position.x) >= travelDistance && !isGravityOn) {
        isGravityOn = true;
        rb.gravityScale = gravity;
      }
    }
  }

  private void DestroyProjectile() {
    beingDestroyed = true;
    rb.gravityScale = 0.0f;
    rb.velocity = new Vector2(0.0f, 0.0f);
    animator.SetBool("fizzle", true);
    Destroy(gameObject, 0.35f);
  }

  public void FireProjectile(float speed, float travelDistance, int damage, string type) {
    this.speed = speed;
    this.travelDistance = travelDistance;
    attackDetails.damageAmount = damage;
    attackDetails.type = type;
  }

  private void OnDrawGizmos() {
    Gizmos.DrawWireSphere(hitboxPosition.position, hitboxRadius);
  }
}