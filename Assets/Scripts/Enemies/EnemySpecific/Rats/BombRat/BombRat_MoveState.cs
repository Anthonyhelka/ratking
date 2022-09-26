using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRat_MoveState : MoveState {
  private BombRat bombRat;

  public BombRat_MoveState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_MoveState stateData, BombRat bombRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.bombRat = bombRat;
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
      stateMachine.ChangeState(bombRat.playerDetectedState);
    } else if (isDetectingWall || !isDetectingLedge) {
      bombRat.idleState.SetFlipAfterIdle(true);
      stateMachine.ChangeState(bombRat.idleState);
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
