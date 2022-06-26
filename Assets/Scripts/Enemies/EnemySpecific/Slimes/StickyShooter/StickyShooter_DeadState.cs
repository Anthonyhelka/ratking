using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyShooter_DeadState : DeadState {
  private StickyShooter stickyShooter;

  public StickyShooter_DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData, StickyShooter stickyShooter) : base(entity, stateMachine, animationBoolName, stateData) {
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
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}