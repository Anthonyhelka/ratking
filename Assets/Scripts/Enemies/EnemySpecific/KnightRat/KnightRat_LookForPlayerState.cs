using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRat_LookForPlayerState : LookForPlayerState {
  private KnightRat knightRat;

  public KnightRat_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_LookForPlayerState stateData, KnightRat knightRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.knightRat = knightRat;
  }

  public override void Enter() {
    base.Enter();

    turnImmediately = true;
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isPlayerInMinAggroRange) {
      stateMachine.ChangeState(knightRat.playerDetectedState);
    } else if (isAllTurnsTimeDone) {
      stateMachine.ChangeState(knightRat.moveState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }
}
