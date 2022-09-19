using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRat_IdleState : IdleState {
  private BombRat bombRat;

  public BombRat_IdleState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_IdleState stateData, BombRat bombRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.bombRat = bombRat;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.SetVelocityX(0.0f);

    if (isPlayerInMinAggroRange) {
      stateMachine.ChangeState(bombRat.playerDetectedState);
    } else if (isIdleTimeOver) {
      stateMachine.ChangeState(bombRat.moveState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}