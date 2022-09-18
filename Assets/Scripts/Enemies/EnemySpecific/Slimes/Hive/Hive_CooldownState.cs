using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive_CooldownState : CooldownState {
  private Hive hive;

  public Hive_CooldownState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_CooldownState stateData, Hive hive) : base(entity, stateMachine, animationBoolName, stateData) {
    this.hive = hive;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isCooldownTimeOver) {
      stateMachine.ChangeState(hive.idleState);
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
