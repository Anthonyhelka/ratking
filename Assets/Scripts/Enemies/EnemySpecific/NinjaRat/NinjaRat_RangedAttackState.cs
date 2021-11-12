using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja_RangedAttackState : RangedAttackState {
  private NinjaRat ninjaRat;

  public Ninja_RangedAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_RangedAttackState stateData, NinjaRat ninjaRat) : base(entity, stateMachine, animationBoolName, attackPosition, stateData) {
    this.ninjaRat = ninjaRat;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isAnimationFinished) {
      if (isPlayerInMinAggroRange) {
        stateMachine.ChangeState(ninjaRat.playerDetectedState);
      } else {
        stateMachine.ChangeState(ninjaRat.moveState);
      }
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
