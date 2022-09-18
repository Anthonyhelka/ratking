using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Entity : MonoBehaviour, IDamageable, IKnockbackable {
  public FiniteStateMachine stateMachine;
  public D_Entity entityData;

  public Animator animator { get; private set; }
  public AnimationToStateMachine atsm { get; private set; }
  public Core Core { get; private set; }
  public Seeker seeker { get; private set; }

  private float currentHealth;
  public int lastDamageDirection;
  public float lastDamageTime;
  public Vector2 lastPlayerDetectedPosition;
  public Collider2D lastPlayerTouched;
  public float chargeCooldownTime;
  public float meleeAttackCooldownTime;
  public bool isHurt;
  protected bool isDead;
  public bool willBlock;
  public bool willDodge;
  public bool usesPathfinding;

  private Vector2 velocityWorkspace;

  [SerializeField] private Transform attackCheck;
  [SerializeField] private Transform wallCheck;
  [SerializeField] private Transform ledgeCheck;
  [SerializeField] private Transform playerCheck;
  [SerializeField] private Transform groundCheck;

  public virtual void Awake() {
    stateMachine = new FiniteStateMachine();
    animator = GetComponent<Animator>();
    atsm = GetComponent<AnimationToStateMachine>();
    Core = GetComponentInChildren<Core>();
    if (usesPathfinding) seeker = GetComponent<Seeker>();

    currentHealth = entityData.maxHealth;
  }

  public virtual void Update() {
    Core.LogicUpdate();
    
    stateMachine.currentState.LogicUpdate();

    animator.SetFloat("yVelocity", Core.Movement.RB.velocity.y);
  }

  public virtual void FixedUpdate() {
    stateMachine.currentState.PhysicsUpdate();
  }

  public virtual void SetPosition(Vector2 position) {
    transform.position = position;
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
      GameObject blockParticle = Instantiate(entityData.blockParticle, (transform.position + (Vector3)attackDetails.position) / 2, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
      Destroy(blockParticle, 0.35f);
    } else {
      currentHealth -= attackDetails.damageAmount;

      GameObject hitParticle = Instantiate(entityData.hitParticle, (transform.position + (Vector3)attackDetails.position) / 2, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
      Destroy(hitParticle, 0.35f);

      lastDamageTime = Time.time;
      lastDamageDirection = attackDetails.position.x <= transform.position.x ? -1 : 1;

      if (lastDamageDirection != Core.Movement.FacingDirection) {
        Core.Movement.Flip();
      }

      if (currentHealth <= 0) {
        isDead = true;
      }
    }
  }

  public virtual void Knockback(Vector2 angle, float strength, int direction) {
    isHurt = true;
  }

  public virtual void OnDrawGizmos() {
    Core.DrawGizmos();
    // // Player Min & Max Check
    Gizmos.DrawWireCube(playerCheck.position, entityData.minAggroDistance);
    Gizmos.DrawWireCube(playerCheck.position, entityData.maxAggroDistance);

    // // Attack Range Check
    Gizmos.DrawWireSphere(attackCheck.position, entityData.closeRangeActionDistance);

    // // Touch Damage Check
    Gizmos.DrawWireCube(playerCheck.position, entityData.touchDamageDistance);
  }
}
