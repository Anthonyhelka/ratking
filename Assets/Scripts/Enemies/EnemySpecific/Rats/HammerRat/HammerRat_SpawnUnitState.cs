using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerRat_SpawnUnitState : SpawnUnitState {
  private HammerRat hammerRat;

  public HammerRat_SpawnUnitState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_SpawnUnitState stateData, HammerRat hammerRat) : base(entity, stateMachine, animationBoolName, attackPosition, stateData) {
    this.hammerRat = hammerRat;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

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

  public override void TriggerAttack() {
    base.TriggerAttack();
  }

  public override void FinishAttack() {
    base.FinishAttack();

    stateMachine.ChangeState(hammerRat.idleState);
  }
}