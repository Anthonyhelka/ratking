using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueFly_PathfindState : PathfindState {
  private PlagueFly plagueFly;

  public PlagueFly_PathfindState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PathfindState stateData, PlagueFly plagueFly) : base(entity, stateMachine, animationBoolName, stateData) {
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
    
    if (!isPlayerInMaxAggroRange) {
      stateMachine.ChangeState(plagueFly.idleState);
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