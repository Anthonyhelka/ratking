using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
  public FiniteStateMachine stateMachine;
  public D_Entity entityData;

  public Rigidbody2D rb { get; private set; }
  public Animator animator { get; private set; }
  public GameObject alive { get; private set; }
  public AnimationToStateMachine atsm { get; private set; }

  private float currentHealth;
  private int lastDamageDirection;
  public float meleeAttackCooldownTime;
  protected bool isDead;

  public int facingDirection { get; private set; }
  private Vector2 velocityWorkspace;

  [SerializeField] private Transform wallCheck;
  [SerializeField] private Transform ledgeCheck;
  [SerializeField] private Transform playerCheck;

  public virtual void Start() {
    stateMachine = new FiniteStateMachine();
    alive = transform.Find("Alive").gameObject;
    rb = alive.GetComponent<Rigidbody2D>();
    animator = alive.GetComponent<Animator>();
    atsm = alive.GetComponent<AnimationToStateMachine>();
    
    currentHealth = entityData.maxHealth;
    facingDirection = 1;
  }

  public virtual void Update() {
    stateMachine.currentState.LogicUpdate();
  }

  public virtual void FixedUpdate() {
    stateMachine.currentState.PhysicsUpdate();
  }

  public virtual void SetVelocity(float velocity) {
    velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
    rb.velocity = velocityWorkspace;
  }

  public virtual bool CheckWall() {
    return Physics2D.Raycast(wallCheck.position, alive.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
  }

  public virtual bool CheckLedge() {
    return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
  }

  public virtual bool CheckPlayerInMinAggroRange() {
    return Physics2D.Raycast(playerCheck.position, alive.transform.right, entityData.minAggroDistance, entityData.whatIsPlayer);
  }

  public virtual bool CheckPlayerInMaxAggroRange() {
    return Physics2D.Raycast(playerCheck.position, alive.transform.right, entityData.maxAggroDistance, entityData.whatIsPlayer);
  }

  public virtual bool CheckPlayerInCloseRangeAction() {
    return Physics2D.Raycast(playerCheck.position, alive.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
  }

  public virtual bool CheckMeleeAttackCooldown() {
    return Time.time >= meleeAttackCooldownTime;
  }

  public virtual void Damage(AttackDetails attackDetails) {
    currentHealth -= attackDetails.damageAmount;
    Debug.Log(attackDetails.damageAmount);
    DamageHop(entityData.damageHopSpeed);

    GameObject hitParticle = Instantiate(entityData.hitParticle, alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
    Destroy(hitParticle, 0.35f);

    if (attackDetails.position.x > alive.transform.position.x) {
      lastDamageDirection = -1;
    } else {
      lastDamageDirection = 1;
    }

    if (currentHealth <= 0) {
      isDead = true;
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
    // Wall & Ledge Check
    Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
    Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
    // Player Min & Max Check
    Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(alive.transform.right * entityData.minAggroDistance), 0.1f);
    Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(alive.transform.right * entityData.maxAggroDistance), 0.1f);
    // Attack Range Check
    Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(alive.transform.right * entityData.closeRangeActionDistance), 0.1f);
  }
}
