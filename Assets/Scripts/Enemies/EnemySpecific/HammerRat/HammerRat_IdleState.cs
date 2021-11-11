using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerRat_IdleState : IdleState {
  private HammerRat hammerRat;

  public HammerRat_IdleState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_IdleState stateData, HammerRat hammerRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.hammerRat = hammerRat;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    entity.SetVelocity(0.0f);
    
    if (isPlayerInMinAggroRange) {
      stateMachine.ChangeState(hammerRat.playerDetectedState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
