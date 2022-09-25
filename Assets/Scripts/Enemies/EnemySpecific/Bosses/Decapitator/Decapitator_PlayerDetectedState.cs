using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decapitator_PlayerDetectedState : PlayerDetectedState {
  private Decapitator decapitator;

  public Decapitator_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PlayerDetectedState stateData, Decapitator decapitator) : base(entity, stateMachine, animationBoolName, stateData) {
    this.decapitator = decapitator;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (performLongRangeAction) {
      stateMachine.ChangeState(decapitator.chargeState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
