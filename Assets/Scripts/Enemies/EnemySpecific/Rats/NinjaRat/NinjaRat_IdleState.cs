using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaRat_IdleState : IdleState {
  private NinjaRat ninjaRat;

  public NinjaRat_IdleState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_IdleState stateData, NinjaRat ninjaRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.ninjaRat = ninjaRat;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.SetVelocityX(0.0f);

    if (isPlayerInMinAggroRange) {
      stateMachine.ChangeState(ninjaRat.playerDetectedState);
    } else if (isIdleTimeOver) {
      stateMachine.ChangeState(ninjaRat.moveState);
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