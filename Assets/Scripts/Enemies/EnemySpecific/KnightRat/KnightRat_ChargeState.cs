using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRat_ChargeState : ChargeState {
  private KnightRat knightRat;

  public KnightRat_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_ChargeState stateData, KnightRat knightRat) : base(entity, stateMachine, animationBoolName, stateData) {
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
    } else if (isChargeTimeOver) {
      stateMachine.ChangeState(knightRat.moveState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }
  public override void DoChecks() {
    base.DoChecks();
  }
}
