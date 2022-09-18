using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueFly_PlayerDetectedState : PlayerDetectedState {
  private PlagueFly plagueFly;

  public PlagueFly_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PlayerDetectedState stateData, PlagueFly plagueFly) : base(entity, stateMachine, animationBoolName, stateData) {
    this.plagueFly = plagueFly;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (performLongRangeAction) {
      stateMachine.ChangeState(plagueFly.pathfindState);
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
