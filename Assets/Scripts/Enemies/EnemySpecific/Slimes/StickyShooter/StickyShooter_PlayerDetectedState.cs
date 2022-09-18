using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyShooter_PlayerDetectedState : PlayerDetectedState {
  private StickyShooter stickyShooter;

  public StickyShooter_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PlayerDetectedState stateData, StickyShooter stickyShooter) : base(entity, stateMachine, animationBoolName, stateData) {
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

    core.Movement.SetVelocityZero();

    if (Time.time > stickyShooter.rangedAttackState.startTime + stickyShooter.rangedAttackStateData.attackCooldown) {
      stateMachine.ChangeState(stickyShooter.rangedAttackState);
    } else if (!isPlayerInMaxAggroRange) {
      stateMachine.ChangeState(stickyShooter.idleState);
    }

    if (isTouchingPlayer) {
      AttackDetails attackDetails;
      attackDetails.position = entity.transform.position;
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