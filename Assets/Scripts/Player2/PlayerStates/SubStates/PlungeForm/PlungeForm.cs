using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlungeForm : MonoBehaviour {
  private Rigidbody2D rb;
  private BoxCollider2D bc;
  public GameObject player;
  private AttackDetails attackDetails;
  private float speed;
  private float travelDistance;
  private float startPositionX;
  private bool hasHitGround;
  private Vector2 groundHitPosition;
  private Vector2 direction;
  [SerializeField] private float hitboxRadius;
  [SerializeField] private Transform hitboxPosition;
  [SerializeField] private LayerMask whatIsPlayer;
  [SerializeField] private LayerMask whatIsEnemy;
  [SerializeField] private LayerMask whatIsGround;

  private void Start() {
    rb = GetComponent<Rigidbody2D>();
    bc = GetComponent<BoxCollider2D>();
    player = GameObject.Find("Player");
    startPositionX = transform.position.x;
    rb.gravityScale = 1.0f;
    bc.enabled = false;
    rb.velocity = direction * speed;
  }

  private void Update() {
    attackDetails.position = transform.position;
  }

  private void FixedUpdate() {
    float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    // RaycastHit2D playerHit = Physics2D.Raycast(hitboxPosition.position, Vector2.right, hitboxRadius, whatIsPlayer);
    // RaycastHit2D enemyHit = Physics2D.Raycast(hitboxPosition.position, Vector2.right, hitboxRadius, whatIsEnemy);
    // RaycastHit2D groundHit = Physics2D.Raycast(hitboxPosition.position, Vector2.right, hitboxRadius, whatIsGround);
    Collider2D playerHit = Physics2D.OverlapCircle(hitboxPosition.position, hitboxRadius, whatIsPlayer);
    Collider2D enemyHit = Physics2D.OverlapCircle(hitboxPosition.position, hitboxRadius, whatIsEnemy);
    Collider2D groundHit = Physics2D.OverlapCircle(hitboxPosition.position, hitboxRadius, whatIsGround);
    if (groundHit && !hasHitGround) {
      hasHitGround = true;
      // groundHitPosition = groundHit.point;
    }
    if (hasHitGround) {
      rb.velocity = new Vector2(0.0f, 0.0f);
      rb.gravityScale = 0.0f;
      // transform.position = groundHitPosition;
      rb.isKinematic = true;
      bc.enabled = true;
      transform.Rotate(0.0f, 0.0f, 0.0f);
    }
    // RaycastHit2D playerHit = Physics2D.Raycast(hitboxPosition.position, Vector2.right, hitboxRadius, whatIsPlayer);
    // RaycastHit2D enemyHit = Physics2D.Raycast(hitboxPosition.position, Vector2.right, hitboxRadius, whatIsEnemy);
    // RaycastHit2D groundHit = Physics2D.Raycast(hitboxPosition.position, Vector2.right, hitboxRadius, whatIsGround);
    // // Collider2D playerHit = Physics2D.OverlapCircle(hitboxPosition.position, hitboxRadius, whatIsPlayer);
    // // Collider2D enemyHit = Physics2D.OverlapCircle(hitboxPosition.position, hitboxRadius, whatIsEnemy);
    // // Collider2D groundHit = Physics2D.OverlapCircle(hitboxPosition.position, hitboxRadius, whatIsGround);
    // // if (groundHit && !hasHitGround) {
    // //   hasHitGround = true;
    // //   groundHitPosition = groundHit.transform.position;
    // // }
    // if (groundHit) {
    //   rb.velocity = new Vector2(0.0f, 0.0f);
    //   rb.gravityScale = 0.0f;
    //   bc.enabled = true;
    //   transform.position = groundHit.point;
    //   transform.Rotate(0.0f, 0.0f, 0.0f);
    // }
    // if (enemyHit) {
    //   enemyHit.transform.parent.SendMessage("Damage", attackDetails);
    // }

    // if (Mathf.Abs(startPositionX - transform.position.x) >= travelDistance && goingOut) {
    //   goingOut = false;
    //   transform.Rotate(0.0f, 180.0f, 0.0f);
    // }

    // if (goingOut) {
    //   rb.velocity = transform.right * speed;
    // } else {
    //   Vector2 direction = (player.transform.position - transform.position).normalized;
    //   rb.velocity = direction * speed * 1.25f;
    // }

    // if (!goingOut && playerHit) {
    //   player.GetComponent<Player>().BoomerangThrowState.CaughtBoomerang();
    //   Destroy(gameObject);
    // }
  }

  public void FirePlungeform(float speed, float travelDistance, int damage, int facingDirection) {
    this.speed = speed;
    this.travelDistance = travelDistance;
    attackDetails.damageAmount = damage;
    direction = new Vector2(facingDirection, 1.0f);
  }

  private void OnDrawGizmos() {
    // Gizmos.DrawRay(hitboxPosition.position, Vector2.right * hitboxRadius);
    Gizmos.DrawWireSphere(hitboxPosition.position, hitboxRadius);
  }
}
