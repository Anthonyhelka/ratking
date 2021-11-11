using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerRat_BlockState : BlockState {
  private HammerRat hammerRat;

  protected bool isTouchingPlayer;

  public HammerRat_BlockState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_BlockState stateData, HammerRat hammerRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.hammerRat = hammerRat;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();

    entity.willBlock = false;
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isMinBlockTimeOver) {
      if (performCloseRangeAction) {
        stateMachine.ChangeState(hammerRat.meleeAttackState);
      } else if (isPlayerInMinAggroRange) {
        stateMachine.ChangeState(hammerRat.playerDetectedState);
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

    entity.willBlock = entity.lastPlayerDetectedPosition.y > entity.alive.transform.position.y + 0.4f;
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isTouchingPlayer = entity.CheckTouchingPlayer();
  }
}