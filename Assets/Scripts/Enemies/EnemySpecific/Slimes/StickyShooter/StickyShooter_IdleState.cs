using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyShooter_IdleState : IdleState {
  private StickyShooter stickyShooter;

  public StickyShooter_IdleState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_IdleState stateData, StickyShooter stickyShooter) : base(entity, stateMachine, animationBoolName, stateData) {
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

    if (isPlayerInMinAggroRange) {
      stateMachine.ChangeState(stickyShooter.playerDetectedState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}