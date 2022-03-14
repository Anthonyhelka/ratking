using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportState : State {
  protected D_TeleportState stateData;

  protected bool isPlayerInMinAggroRange;
  protected bool performCloseRangeAction;
  protected Vector2 lastPlayerDetectedPosition;
  protected bool isTeleportTimeOver;
  
  public TeleportState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_TeleportState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }
  
  public override void Enter() {
    base.Enter();

    isTeleportTimeOver = false;
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (Time.time >= startTime + stateData.teleportTime) {
      isTeleportTimeOver = true;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    lastPlayerDetectedPosition = entity.lastPlayerDetectedPosition;
  }
}
