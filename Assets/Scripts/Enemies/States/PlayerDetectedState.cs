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
  protected bool isGrounded;
  protected bool isTouchingPlayer;

  public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }

  public override void Enter() {
    base.Enter();

    performLongRangeAction = false;
    core.Movement.SetVelocityX(0.0f);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.SetVelocityX(0.0f);

    if (stateData.shouldFlip && core.Movement.FacingDirection != (entity.lastPlayerDetectedPosition.x <= entity.transform.position.x ? -1 : 1)) {
      core.Movement.Flip();
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
    isDetectingWall = core.CollisionSenses.WallFront;
    isDetectingLedge = core.CollisionSenses.LedgeVertical;
    isGrounded = core.CollisionSenses.Grounded;
    isTouchingPlayer = entity.CheckTouchingPlayer();
  }
}
