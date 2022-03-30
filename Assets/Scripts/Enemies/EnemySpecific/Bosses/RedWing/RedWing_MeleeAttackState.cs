using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWing_MeleeAttackState : MeleeAttackState {
  private RedWing redWing;

  public RedWing_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_MeleeAttackState stateData, RedWing redWing) : base(entity, stateMachine, animationBoolName, attackPosition, stateData) {
    this.redWing = redWing;
  }

  public override void Enter() {
    base.Enter();

    attackPosition.gameObject.GetComponent<Animator>().SetBool("meleeAttack", true);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
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
