using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedTrap_MeleeAttackState : MeleeAttackState {
  private InfectedTrap infectedTrap;

  public InfectedTrap_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_MeleeAttackState stateData, InfectedTrap infectedTrap) : base(entity, stateMachine, animationBoolName, attackPosition, stateData) {
    this.infectedTrap = infectedTrap;
  }

  public override void Enter() {
    base.Enter();

    core.Combat.canBeKnockedBack = false;
  }

  public override void Exit() {
    base.Exit();

    core.Combat.canBeKnockedBack = true;
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isAnimationFinished) {
      stateMachine.ChangeState(infectedTrap.deadState);
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
