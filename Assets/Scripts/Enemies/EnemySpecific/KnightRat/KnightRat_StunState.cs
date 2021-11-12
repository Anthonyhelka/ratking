using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRat_StunState : StunState {
  private KnightRat knightRat;

  public KnightRat_StunState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_StunState stateData, KnightRat knightRat) : base(entity, stateMachine, animationBoolName, stateData) {
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

    if (isStunTimeOver) {
      if (performCloseRangeAction) {
        if (Time.time > knightRat.meleeAttackState.startTime + knightRat.meleeAttackStateData.attackCooldown) {
          stateMachine.ChangeState(knightRat.meleeAttackState);
        }
      } else if (isPlayerInMinAggroRange) {
        stateMachine.ChangeState(knightRat.playerDetectedState);
      } else {
        stateMachine.ChangeState(knightRat.moveState);
      }
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
