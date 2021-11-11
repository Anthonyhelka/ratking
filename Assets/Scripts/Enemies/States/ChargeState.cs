using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State {
  protected D_ChargeState stateData;

  protected bool isPlayerInMinAggroRange;
  protected bool performCloseRangeAction;
  protected bool isTouchingPlayer;
  protected bool isChargeTimeOver;
  
  public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_ChargeState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }
  
  public override void Enter() {
    base.Enter();

    isChargeTimeOver = false;
    entity.SetVelocity(stateData.chargeSpeed);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (Time.time >= startTime + stateData.chargeTime) {
      isChargeTimeOver = true;
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

    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    performCloseRangeAction = entity.CheckPlayerInCloseRangeAction() && entity.CheckMeleeAttackCooldown();
    isTouchingPlayer = entity.CheckTouchingPlayer();
  }
}