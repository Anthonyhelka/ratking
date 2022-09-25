using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decapitator_DeadState : DeadState {
  private Decapitator decapitator;

  public Decapitator_DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData, Decapitator decapitator) : base(entity, stateMachine, animationBoolName, stateData) {
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
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
