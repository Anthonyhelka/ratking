using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRat_FleeState : FleeState {
  private BombRat bombRat;

  public BombRat_FleeState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_FleeState stateData, BombRat bombRat) : base(entity, stateMachine, animationBoolName, stateData) {
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

    if (!isPlayerInMaxAggroRange) {
      stateMachine.ChangeState(bombRat.moveState);
    } else if (Time.time > bombRat.spawnUnitState.startTime + bombRat.spawnUnitStateData.spawnCooldown) {
      stateMachine.ChangeState(bombRat.spawnUnitState);
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