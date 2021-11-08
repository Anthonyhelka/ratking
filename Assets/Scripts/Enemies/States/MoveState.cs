using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State {
  protected D_MoveState stateData;

  protected bool isDetectingWall;
  protected bool isDetectingLedge;

  public MoveState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_MoveState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }

  public override void Enter() {
    base.Enter();
    entity.SetVelocity(stateData.movementSpeed);

    isDetectingWall = entity.CheckWall();
    isDetectingLedge = entity.CheckLedge();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();

    isDetectingWall = entity.CheckWall();
    isDetectingLedge = entity.CheckLedge();
  }
}
