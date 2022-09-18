using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State {
  protected D_MoveState stateData;

  protected bool isDetectingWall;
  protected bool isDetectingLedge;
  protected bool isPlayerInMinAggroRange;
  protected bool isTouchingPlayer;
  
  public MoveState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_MoveState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }

  public override void Enter() {
    base.Enter();

    core.Movement.SetVelocityX(stateData.movementSpeed * core.Movement.FacingDirection);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.SetVelocityX(stateData.movementSpeed * core.Movement.FacingDirection);
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isDetectingWall = core.CollisionSenses.WallFront;
    isDetectingLedge = core.CollisionSenses.LedgeVertical;
    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    isTouchingPlayer = entity.CheckTouchingPlayer();
  }
}
