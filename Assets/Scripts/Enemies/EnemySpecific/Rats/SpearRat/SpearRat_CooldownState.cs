using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearRat_CooldownState : CooldownState {
  private SpearRat spearRat;

  public SpearRat_CooldownState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_CooldownState stateData, SpearRat spearRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.spearRat = spearRat;
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
      stateMachine.ChangeState(spearRat.idleState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
