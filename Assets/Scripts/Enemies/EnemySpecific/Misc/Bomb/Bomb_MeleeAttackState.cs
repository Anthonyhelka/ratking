using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_MeleeAttackState : MeleeAttackState {
  private Bomb bomb;

  public Bomb_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_MeleeAttackState stateData, Bomb bomb) : base(entity, stateMachine, animationBoolName, attackPosition, stateData) {
    this.bomb = bomb;
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
      stateMachine.ChangeState(bomb.deadState);
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
