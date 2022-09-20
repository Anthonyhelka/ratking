using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRat_PlayerDetectedState : PlayerDetectedState {
  private BombRat bombRat;

  public BombRat_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PlayerDetectedState stateData, BombRat bombRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.bombRat = bombRat;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.SetVelocityX(0.0f);

    if (performLongRangeAction) {
      core.Movement.Flip();
      stateMachine.ChangeState(bombRat.fleeState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
