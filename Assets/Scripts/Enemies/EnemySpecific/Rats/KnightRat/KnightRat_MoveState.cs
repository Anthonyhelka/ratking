using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRat_MoveState : MoveState {
  private KnightRat knightRat;

  public KnightRat_MoveState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_MoveState stateData, KnightRat knightRat) : base(entity, stateMachine, animationBoolName, stateData) {
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
    } else if (isDetectingWall || !isDetectingLedge) {
      knightRat.idleState.SetFlipAfterIdle(true);
      stateMachine.ChangeState(knightRat.idleState);
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
