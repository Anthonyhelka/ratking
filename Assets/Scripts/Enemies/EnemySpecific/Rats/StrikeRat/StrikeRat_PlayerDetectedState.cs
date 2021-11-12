using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeRat_PlayerDetectedState : PlayerDetectedState {
  private StrikeRat strikeRat;

  public StrikeRat_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PlayerDetectedState stateData, StrikeRat strikeRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.strikeRat = strikeRat;
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
      if (Time.time > strikeRat.meleeAttackState.startTime + strikeRat.meleeAttackStateData.attackCooldown) {
        stateMachine.ChangeState(strikeRat.meleeAttackState);
      }
    } else if (performLongRangeAction) {
      // Do Teleport
      stateMachine.ChangeState(strikeRat.idleState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
