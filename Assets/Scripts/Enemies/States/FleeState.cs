using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : State {
  protected D_FleeState stateData;

  protected bool isPlayerInMinAggroRange;
  protected bool isPlayerInMaxAggroRange;
  protected bool performCloseRangeAction;
  protected bool isDetectingWall;
  protected bool isDetectingLedge;
  protected bool isTouchingPlayer;

  public FleeState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_FleeState stateData) : base(entity, stateMachine, animationBoolName) {
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
    
    if (isDetectingWall || (!isDetectingLedge && !core.Combat.isKnockbackActive)) {
      core.Movement.Flip();
    } else {
      core.Movement.SetVelocityX(stateData.fleeSpeed * core.Movement.FacingDirection);
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
    isTouchingPlayer = entity.CheckTouchingPlayer();
  }
}
