using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerRat_PlayerDetectedState : PlayerDetectedState {
  private HammerRat hammerRat;

  public HammerRat_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PlayerDetectedState stateData, HammerRat hammerRat) : base(entity, stateMachine, animationBoolName, stateData) {
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

    if (performCloseRangeAction) {
      if (Time.time > hammerRat.meleeAttackState.startTime + hammerRat.meleeAttackStateData.attackCooldown) {
        stateMachine.ChangeState(hammerRat.meleeAttackState);
      }
    } else if (entity.lastPlayerDetectedPosition.y > entity.alive.transform.position.y + 0.4f) {
      stateMachine.ChangeState(hammerRat.blockState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}

