using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyShooter_RangedAttackState : RangedAttackState {
  private StickyShooter stickyShooter;

  public StickyShooter_RangedAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_RangedAttackState stateData, StickyShooter stickyShooter) : base(entity, stateMachine, animationBoolName, attackPosition, stateData) {
    this.stickyShooter = stickyShooter;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isAnimationFinished) {
      if (isPlayerInMinAggroRange) {
        stateMachine.ChangeState(stickyShooter.playerDetectedState);
      } else {
        stateMachine.ChangeState(stickyShooter.idleState);
      }
    }

    if (isTouchingPlayer) {
      AttackDetails attackDetails;
      attackDetails.position = entity.alive.transform.position;
      attackDetails.damageAmount = entity.entityData.touchDamageAmount;
      attackDetails.type = entity.entityData.type;
      entity.lastPlayerTouched.transform.SendMessage("Damage", attackDetails);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
