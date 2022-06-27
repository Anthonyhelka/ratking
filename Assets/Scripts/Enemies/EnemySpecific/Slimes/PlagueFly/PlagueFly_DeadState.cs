using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueFly_DeadState : DeadState {
  private PlagueFly plagueFly;

  public PlagueFly_DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData, PlagueFly plagueFly) : base(entity, stateMachine, animationBoolName, stateData) {
    this.plagueFly = plagueFly;
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