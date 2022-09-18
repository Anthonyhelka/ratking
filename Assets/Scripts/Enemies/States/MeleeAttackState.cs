using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState {
  protected D_MeleeAttackState stateData;
  
  protected AttackDetails attackDetails;
  protected float attackCooldownTime;
  protected bool isTouchingPlayer;

  public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_MeleeAttackState stateData) : base(entity, stateMachine, animationBoolName, attackPosition) {
    this.stateData = stateData;
  }
  
  public override void Enter() {
    base.Enter();

    attackDetails.position = entity.transform.position;
    attackDetails.damageAmount = stateData.attackDamage;
    attackDetails.type = entity.entityData.type;
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (core.Movement.FacingDirection != (entity.lastPlayerDetectedPosition.x <= entity.transform.position.x ? -1 : 1)) {
      core.Movement.Flip();
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isTouchingPlayer = entity.CheckTouchingPlayer();
  }

  public override void TriggerAttack() {
    base.TriggerAttack();

    Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);
    foreach (Collider2D collider in detectedObjects) {
      IDamageable damageable = collider.GetComponent<IDamageable>();

      if (damageable != null) {
        damageable.Damage(attackDetails);
      }
    }
  }
  
  public override void FinishAttack() {
    base.FinishAttack();

    entity.meleeAttackCooldownTime = Time.time + stateData.attackCooldown;
  }
}
