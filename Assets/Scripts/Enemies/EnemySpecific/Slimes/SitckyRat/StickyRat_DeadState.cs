using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyRat_DeadState : DeadState {
  private StickyRat stickyRat;

  public StickyRat_DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData, StickyRat stickyRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.stickyRat = stickyRat;
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