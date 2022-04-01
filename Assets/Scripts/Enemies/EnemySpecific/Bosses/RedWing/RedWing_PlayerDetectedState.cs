using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWing_PlayerDetectedState : PlayerDetectedState {
  private RedWing redWing;

  public RedWing_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PlayerDetectedState stateData, RedWing redWing) : base(entity, stateMachine, animationBoolName, stateData) {
    this.redWing = redWing;
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