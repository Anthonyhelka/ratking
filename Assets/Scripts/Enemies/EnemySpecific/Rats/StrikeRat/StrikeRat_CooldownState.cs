using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeRat_CooldownState : CooldownState {
  private StrikeRat strikeRat;

  public StrikeRat_CooldownState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_CooldownState stateData, StrikeRat strikeRat) : base(entity, stateMachine, animationBoolName, stateData) {
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

    if (isCooldownTimeOver) {
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
