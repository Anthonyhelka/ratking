using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeRat_TeleportState : TeleportState {
  private StrikeRat strikeRat;

  public StrikeRat_TeleportState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_TeleportState stateData, StrikeRat strikeRat) : base(entity, stateMachine, animationBoolName, stateData) {
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

    if (isTeleportTimeOver) {
      entity.SetPosition(lastPlayerDetectedPosition);
      stateMachine.ChangeState(strikeRat.meleeAttackState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
