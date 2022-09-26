using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedTrap_IdleState : IdleState {
  private InfectedTrap infectedTrap;

  public InfectedTrap_IdleState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_IdleState stateData, InfectedTrap infectedTrap) : base(entity, stateMachine, animationBoolName, stateData) {
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

    if (performCloseRangeAction || isTouchingPlayer) {
      stateMachine.ChangeState(infectedTrap.meleeAttackState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
