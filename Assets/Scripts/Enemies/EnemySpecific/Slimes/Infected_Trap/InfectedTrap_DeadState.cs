using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedTrap_DeadState : DeadState {
  private InfectedTrap infectedTrap;

  public InfectedTrap_DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData, InfectedTrap infectedTrap) : base(entity, stateMachine, animationBoolName, stateData) {
    this.infectedTrap = infectedTrap;
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