using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRat_PlayerDetectedState : PlayerDetectedState {
  private KnightRat knightRat;

  public KnightRat_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PlayerDetectedState stateData, KnightRat knightRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.knightRat = knightRat;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (performCloseRangeAction) {
      stateMachine.ChangeState(knightRat.meleeAttackState);
    } else if (performLongRangeAction) {
      stateMachine.ChangeState(knightRat.chargeState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}