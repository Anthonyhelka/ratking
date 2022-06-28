using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnitState : AttackState {
  protected D_SpawnUnitState stateData;

  protected GameObject unit;

  protected bool isTouchingPlayer;

  public SpawnUnitState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_SpawnUnitState stateData) : base(entity, stateMachine, animationBoolName, attackPosition) {
    this.stateData = stateData;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isTouchingPlayer = entity.CheckTouchingPlayer();
  }

  public override void TriggerAttack() {
    base.TriggerAttack();

    unit = GameObject.Instantiate(stateData.unit, attackPosition.position, attackPosition.rotation);
  }

  public override void FinishAttack() {
    base.FinishAttack();
  }
}