using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaRat_MoveState : MoveState {
  private NinjaRat ninjaRat;

  public NinjaRat_MoveState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_MoveState stateData, NinjaRat ninjaRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.ninjaRat = ninjaRat;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isPlayerInMinAggroRange) {
      stateMachine.ChangeState(ninjaRat.playerDetectedState);
    } else if (isDetectingWall || !isDetectingLedge) {
      ninjaRat.idleState.SetFlipAfterIdle(true);
      stateMachine.ChangeState(ninjaRat.idleState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
