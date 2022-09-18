using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeRat_MeleeAttackState : MeleeAttackState {
  private StrikeRat strikeRat;

  public StrikeRat_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_MeleeAttackState stateData, StrikeRat strikeRat) : base(entity, stateMachine, animationBoolName, attackPosition, stateData) {
    this.strikeRat = strikeRat;
  }

  public override void Enter() {
    base.Enter();

    attackPosition.gameObject.GetComponent<Animator>().SetBool("meleeAttack", true);

    core.Combat.canBeKnockedBack = false;
  }

  public override void Exit() {
    base.Exit();

    core.Combat.canBeKnockedBack = true;
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isAnimationFinished) {
      stateMachine.ChangeState(strikeRat.cooldownState);
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

    attackPosition.gameObject.GetComponent<Animator>().SetBool("meleeAttack", false);
  }
}
