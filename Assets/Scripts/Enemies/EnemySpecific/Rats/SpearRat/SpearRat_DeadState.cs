using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearRat_DeadState : DeadState {
  private SpearRat spearRat;

  public SpearRat_DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData, SpearRat spearRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.spearRat = spearRat;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    Debug.Log("Dead");
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
