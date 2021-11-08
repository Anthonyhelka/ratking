using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State {
  protected D_ChargeState stateData;

  protected bool isPlayerInMinAggroRange;
  protected bool isDetectingWall;
  protected bool isDetectingLedge;

  public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_ChargeState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }
  
  public override void Enter() {
    base.Enter();

    entity.SetVelocity(stateData.chargeSpeed);
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

    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    isDetectingWall = entity.CheckWall();
    isDetectingLedge = entity.CheckLedge();
  }
}
