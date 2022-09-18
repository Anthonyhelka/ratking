using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearRat_ChargeState : ChargeState {
  private SpearRat spearRat;

  public SpearRat_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_ChargeState stateData, SpearRat spearRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.spearRat = spearRat;
  }

  public override void Enter() {
    base.Enter();

    core.Combat.canBeKnockedBack = false;
  }

  public override void Exit() {
    base.Exit();

    core.Combat.canBeKnockedBack = true;
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isChargeTimeOver) {
      stateMachine.ChangeState(spearRat.cooldownState);
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
