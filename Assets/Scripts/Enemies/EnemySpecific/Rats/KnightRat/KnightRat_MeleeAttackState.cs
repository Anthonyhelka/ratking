using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRat_MeleeAttackState : MeleeAttackState {
  private KnightRat knightRat;

  public KnightRat_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_MeleeAttackState stateData, KnightRat knightRat) : base(entity, stateMachine, animationBoolName, attackPosition, stateData) {
    this.knightRat = knightRat;
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
      if (isPlayerInMinAggroRange) {
        stateMachine.ChangeState(knightRat.playerDetectedState);
      } else {
        stateMachine.ChangeState(knightRat.moveState);
      }
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
