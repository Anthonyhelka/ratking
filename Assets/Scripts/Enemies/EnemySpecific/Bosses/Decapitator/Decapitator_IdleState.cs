using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decapitator_IdleState : IdleState {
  private Decapitator decapitator;

  public Decapitator_IdleState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_IdleState stateData, Decapitator decapitator) : base(entity, stateMachine, animationBoolName, stateData) {
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

    if (isPlayerInMinAggroRange) {
      stateMachine.ChangeState(decapitator.playerDetectedState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
