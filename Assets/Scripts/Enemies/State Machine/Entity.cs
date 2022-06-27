using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Entity : MonoBehaviour, IDamageable {
  public FiniteStateMachine stateMachine;
  public D_Entity entityData;

  public Rigidbody2D rb { get; private set; }
  public Animator animator { get; private set; }
  public GameObject alive { get; private set; }
  public AnimationToStateMachine atsm { get; private set; }
  public Seeker seeker { get; private set; }

  private float currentHealth;
  public int lastDamageDirection;
  public float lastDamageTime;
  public Vector2 lastPlayerDetectedPosition;
  public Collider2D lastPlayerTouched;
  public float chargeCooldownTime;
  public float meleeAttackCooldownTime;
  protected bool isDead;
  public bool willBlock;
  public bool willDodge;
  public bool usesPathfinding;

  public int facingDirection { get; private set; }
  private Vector2 velocityWorkspace;

  [SerializeField] private Transform attackCheck;
  [SerializeField] private Transform wallCheck;
  [SerializeField] private Transform ledgeCheck;
  [SerializeField] private Transform playerCheck;
  [SerializeField] private Transform groundCheck;

  public virtual void Start() {
    stateMachine = new FiniteStateMachine();
    alive = transform.Find("Alive").gameObject;
    rb = alive.GetComponent<Rigidbody2D>();
    animator = alive.GetComponent<Animator>();
    atsm = alive.GetComponent<AnimationToStateMachine>();
    if (usesPathfinding) seeker = alive.GetComponent<Seeker>();

    currentHealth = entityData.maxHealth;
    facingDirection = alive.transform.rotation.y == 0.0f ? 1 : -1;
  }

  public virtual void Update() {
    stateMachine.currentState.LogicUpdate();

    animator.SetFloat("yVelocity", rb.velocity.y);
  }

  public virtual void FixedUpdate() {
    stateMachine.currentState.PhysicsUpdate();
  }

  public virtual void SetVelocity(float velocity) {
    velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
    rb.velocity = velocityWorkspace;
  }

  public virtual void SetVelocity(float velocity, Vector2 angle, int direction) {
    angle.Normalize();
    velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
    rb.velocity = velocityWorkspace;
  }

  public void SetVelocity(float velocity, Vector2 direction) {
    velocityWorkspace = direction * velocity;
    rb.velocity = velocityWorkspace;
  }

  public virtual void SetVelocityX(float velocity) {
    velocityWorkspace.Set(velocity, rb.velocity.y);
    rb.velocity = velocityWorkspace;
  }

  public virtual void SetVelocityY(float velocity) {
    velocityWorkspace.Set(rb.velocity.x, velocity);
    rb.velocity = velocityWorkspace;
  }

  public virtual void SetPosition(Vector2 position) {
    alive.transform.position = position;
  }

  public virtual bool CheckWall() {
    return Physics2D.Raycast(wallCheck.position, alive.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
  }

  public virtual bool CheckLedge() {
    return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
  }

  public virtual bool CheckGround() {
    return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);
  }

  public virtual bool CheckPlayerInMinAggroRange() {
    Collider2D[] detectedObjectsMinAggroRange = Physics2D.OverlapBoxAll(playerCheck.position, entityData.minAggroDistance, 0.0f, entityData.whatIsPlayer);
    if (detectedObjectsMinAggroRange.Length > 0) {
      lastPlayerDetectedPosition = detectedObjectsMinAggroRange[0].transform.position;
      return true;
    } else {
      return false;
    }
  }

  public virtual bool CheckPlayerInMaxAggroRange() {
    Collider2D[] detectedObjectsMaxAggroRange = Physics2D.OverlapBoxAll(playerCheck.position, entityData.maxAggroDistance, 0.0f, entityData.whatIsPlayer);
    if (detectedObjectsMaxAggroRange.Length > 0) {
      lastPlayerDetectedPosition = detectedObjectsMaxAggroRange[0].transform.position;
      return true;
    } else {
      return false;
    }
  }

  public virtual bool CheckPlayerInCloseRangeAction() {
    return Physics2D.OverlapCircleAll(attackCheck.position, entityData.closeRangeActionDistance, entityData.whatIsPlayer).Length > 0;
  }

  public virtual bool CheckTouchingPlayer() {
    Collider2D[] detectedObjects = Physics2D.OverlapBoxAll(playerCheck.position, entityData.touchDamageDistance, 0.0f, entityData.whatIsPlayer);
    if (detectedObjects.Length > 0) {
      lastPlayerTouched = detectedObjects[0];
      return true;
    } else {
      lastPlayerTouched = null;
      return false;
    }
  }

  public virtual void Damage(AttackDetails attackDetails) {
    if (Time.time < lastDamageTime + entityData.damageCooldown) { return; }

    if (willDodge) {
      return;
    } else if (willBlock) {
      GameObject blockParticle = Instantiate(entityData.blockParticle, (alive.transform.position + (Vector3)attackDetails.position) / 2, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
      Destroy(blockParticle, 0.35f);
    } else {
      currentHealth -= attackDetails.damageAmount;

      DamageHop(entityData.damageHopSpeed);

      GameObject hitParticle = Instantiate(entityData.hitParticle, (alive.transform.position + (Vector3)attackDetails.position) / 2, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
      Destroy(hitParticle, 0.35f);

      lastDamageTime = Time.time;
      lastDamageDirection = attackDetails.position.x <= alive.transform.position.x ? -1 : 1;

      if (currentHealth <= 0) {
        isDead = true;
      }
    }
  }

  public virtual void DamageHop(float velocity) {
    velocityWorkspace.Set(rb.velocity.x, velocity);
    rb.velocity = velocityWorkspace;
  }

  public virtual void Flip() {
    facingDirection *= -1;
    alive.transform.Rotate(0.0f, 180.0f, 0.0f);
  }

  public virtual void OnDrawGizmos() {
    // Wall, Ledge & Ground Check
    Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
    Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
    Gizmos.DrawWireSphere(groundCheck.position, entityData.groundCheckRadius);

    // Player Min & Max Check
    Gizmos.DrawWireCube(playerCheck.position, entityData.minAggroDistance);
    Gizmos.DrawWireCube(playerCheck.position, entityData.maxAggroDistance);

    // Attack Range Check
    Gizmos.DrawWireSphere(attackCheck.position, entityData.closeRangeActionDistance);

    // Touch Damage Check
    Gizmos.DrawWireCube(playerCheck.position, entityData.touchDamageDistance);
  }
}
