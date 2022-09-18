using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedRat_HurtState : HurtState {
  private InfectedRat infectedRat;

  public InfectedRat_HurtState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_HurtState stateData, InfectedRat infectedRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.infectedRat = infectedRat;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isHurtOver) {
      stateMachine.ChangeState(infectedRat.moveState);
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
