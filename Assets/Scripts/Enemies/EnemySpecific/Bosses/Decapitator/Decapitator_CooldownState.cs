using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decapitator_CooldownState : CooldownState {
  private Decapitator decapitator;

  public Decapitator_CooldownState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_CooldownState stateData, Decapitator decapitator) : base(entity, stateMachine, animationBoolName, stateData) {
    this.decapitator = decapitator;
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
      stateMachine.ChangeState(decapitator.idleState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
