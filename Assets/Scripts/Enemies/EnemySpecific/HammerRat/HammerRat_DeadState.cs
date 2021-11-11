using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerRat_DeadState : DeadState {
  private HammerRat hammerRat;

  public HammerRat_DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData, HammerRat hammerRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.hammerRat = hammerRat;
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