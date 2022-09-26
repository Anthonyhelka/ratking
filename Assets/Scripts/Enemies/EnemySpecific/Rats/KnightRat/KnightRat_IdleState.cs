using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRat_IdleState : IdleState {
  private KnightRat knightRat;

  public KnightRat_IdleState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_IdleState stateData, KnightRat knightRat) : base(entity, stateMachine, animationBoolName, stateData) {
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

    if (isPlayerInMinAggroRange) {
      stateMachine.ChangeState(knightRat.playerDetectedState);
    } else if (isIdleTimeOver) {
      stateMachine.ChangeState(knightRat.moveState);
    }

    if (isTouchingPlayer) {
      AttackDetails attackDetails;
      attackDetails.position = entity.transform.position;
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
  }
}
