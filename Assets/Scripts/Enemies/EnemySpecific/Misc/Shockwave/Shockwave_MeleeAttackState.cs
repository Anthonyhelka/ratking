using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave_MeleeAttackState : MeleeAttackState {
  private Shockwave shockwave;

  public Shockwave_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_MeleeAttackState stateData, Shockwave shockwave) : base(entity, stateMachine, animationBoolName, attackPosition, stateData) {
    this.shockwave = shockwave;
  }

  public override void Enter() {
    base.Enter();

    core.Combat.canBeKnockedBack = false;
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isAnimationFinished) {
      stateMachine.ChangeState(shockwave.deadState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public override void TriggerAttack() {
    base.TriggerAttack();
  }
  
  public override void FinishAttack() {
    base.FinishAttack();
  }
}
