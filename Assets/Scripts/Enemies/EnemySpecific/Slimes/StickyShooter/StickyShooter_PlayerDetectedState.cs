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

    entity.SetVelocity(0.0f);

    if (Time.time > stickyShooter.rangedAttackState.startTime + stickyShooter.rangedAttackStateData.attackCooldown) {
      stateMachine.ChangeState(stickyShooter.rangedAttackState);
    } else if (!isPlayerInMaxAggroRange) {
      stateMachine.ChangeState(stickyShooter.idleState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}