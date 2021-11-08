using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State {
  protected D_PlayerDetectedState stateData;

  protected bool isPlayerInMinAggroRange;
  protected bool isPlayerInMaxAggroRange;
  protected bool performCloseRangeAction;
  protected bool performLongRangeAction;

  public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }

  public override void Enter() {
    base.Enter();

    performLongRangeAction = false;
    entity.SetVelocity(0.0f);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (Time.time >= startTime + stateData.longRangeActionTime) {
      performLongRangeAction = true;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    isPlayerInMaxAggroRange = entity.CheckPlayerInMinAggroRange();
    performCloseRangeAction = entity.CheckPlayerInCloseRangeAction() && entity.CheckMeleeAttackCooldown();
  }
}
