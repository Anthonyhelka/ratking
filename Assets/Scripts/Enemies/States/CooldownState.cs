using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownState : State {
  protected D_CooldownState stateData;

  protected bool isPlayerInMinAggroRange;
  protected bool performCloseRangeAction;
  protected bool isCooldownTimeOver;
  
  public CooldownState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_CooldownState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }
  
  public override void Enter() {
    base.Enter();

    isCooldownTimeOver = false;
    entity.SetVelocity(0.0f);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (Time.time >= startTime + stateData.cooldownTime) {
      isCooldownTimeOver = true;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
  }
}
