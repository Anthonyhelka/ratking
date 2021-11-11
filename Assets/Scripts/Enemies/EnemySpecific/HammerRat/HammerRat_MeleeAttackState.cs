using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerRat_MeleeAttackState : MeleeAttackState {
  private HammerRat hammerRat;
  protected bool isTouchingPlayer;
  
  public HammerRat_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_MeleeAttackState stateData, HammerRat hammerRat) : base(entity, stateMachine, animationBoolName, attackPosition, stateData) {
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

    if (isAnimationFinished) {
      if (isPlayerInMinAggroRange) {
        stateMachine.ChangeState(hammerRat.idleState);
      } else {
        stateMachine.ChangeState(hammerRat.idleState);
      }
    }

    if (isTouchingPlayer) {
      AttackDetails attackDetails;
      attackDetails.position = entity.alive.transform.position;
      attackDetails.damageAmount = entity.entityData.touchDamageAmount;
      attackDetails.type = entity.entityData.type;
      entity.lastPlayerTouched.transform.SendMessage("Damage", attackDetails);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isTouchingPlayer = entity.CheckTouchingPlayer();
  }

  public override void TriggerAttack() {
    base.TriggerAttack();
  }
  
  public override void FinishAttack() {
    base.FinishAttack();
  }
}

