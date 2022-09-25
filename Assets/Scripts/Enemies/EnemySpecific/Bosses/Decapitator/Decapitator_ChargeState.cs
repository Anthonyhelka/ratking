using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decapitator_ChargeState : ChargeState {
  private Decapitator decapitator;

  public Decapitator_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_ChargeState stateData, Decapitator decapitator) : base(entity, stateMachine, animationBoolName, stateData) {
    this.decapitator = decapitator;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (!isChargeTimeOver && isDetectingWall) {
      core.Movement.Flip();
    } else if (isChargeTimeOver && isDetectingWall) {
      core.Movement.Flip();
      stateMachine.ChangeState(decapitator.cooldownState);
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
