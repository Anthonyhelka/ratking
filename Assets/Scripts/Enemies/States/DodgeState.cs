using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State {
  protected D_DodgeState stateData;

  protected bool performCloseRangeAction;
  protected bool isPlayerInMaxAggroRange;
  protected bool isDetectingWall;
  protected bool isDetectingLedge;
  protected bool isGrounded;
  protected bool isTouchingPlayer;
  protected bool isDodgeOver;

  public DodgeState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DodgeState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }
  
  public override void Enter() {
    base.Enter();

    isDodgeOver = false;
    entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, entity.facingDirection);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (Time.time >= startTime + stateData.dodgeTime && isGrounded) {
      isDodgeOver = true;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
    isDetectingWall = entity.CheckWall();
    isDetectingLedge = entity.CheckLedge();
    isGrounded = entity.CheckGround();
    isTouchingPlayer = entity.CheckTouchingPlayer();
  }
}