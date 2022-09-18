using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive_SpawnUnitState : SpawnUnitState {
  private Hive hive;

  public Hive_SpawnUnitState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_SpawnUnitState stateData, Hive hive) : base(entity, stateMachine, animationBoolName, attackPosition, stateData) {
    this.hive = hive;
  }

  public override void Enter() {
    base.Enter();

    Debug.Log("here");
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

    stateMachine.ChangeState(hive.cooldownState);
  }
}