using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearRat_PlayerDetectedState : PlayerDetectedState {
  private SpearRat spearRat;

  public SpearRat_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PlayerDetectedState stateData, SpearRat spearRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.spearRat = spearRat;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (performLongRangeAction && Time.time > entity.chargeCooldownTime) {
      stateMachine.ChangeState(spearRat.chargeState);
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
  }
}
