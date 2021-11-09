using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRat_HurtState : HurtState {
  private KnightRat knightRat;

  public KnightRat_HurtState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_HurtState stateData, KnightRat knightRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.knightRat = knightRat;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isHurtTimeOver) {
      stateMachine.ChangeState(knightRat.lookForPlayerState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
