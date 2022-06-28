using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State {
  protected D_PlayerDetectedState stateData;

  protected bool isPlayerInMinAggroRange;
  protected bool isPlayerInMaxAggroRange;
  protected bool performCloseRangeAction;
  protected bool performLongRangeAction;
  protected bool isDetectingWall;
  protected bool isDetectingLedge;
  protected bool isTouchingPlayer;

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

    if (stateData.shouldFlip && entity.facingDirection != (entity.lastPlayerDetectedPosition.x <= entity.alive.transform.position.x ? -1 : 1)) {
      entity.Flip();
    }

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
    isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
    performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    isDetectingWall = entity.CheckWall();
    isDetectingLedge = entity.CheckLedge();
    isTouchingPlayer = entity.CheckTouchingPlayer();
  }
}
