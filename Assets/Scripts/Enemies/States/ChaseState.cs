using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State {
  protected D_ChaseState stateData;

  protected bool isPlayerInMinAggroRange;
  protected bool isPlayerInMaxAggroRange;
  protected bool performCloseRangeAction;
  protected bool isTouchingPlayer;
  protected bool willTurn;
  protected float turnStartTime;

  public ChaseState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_ChaseState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }
  
  public override void Enter() {
    base.Enter();
    
    core.Movement.SetVelocityX(stateData.chaseSpeed * core.Movement.FacingDirection);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.SetVelocityX(stateData.chaseSpeed * core.Movement.FacingDirection);

    if (core.Movement.FacingDirection != (entity.lastPlayerDetectedPosition.x <= entity.transform.position.x ? -1 : 1) && !willTurn) {
      willTurn = true;
      turnStartTime = Time.time;
    }

    if (willTurn) {
      if (Time.time > turnStartTime + stateData.turnDelayTime) {
        willTurn = false;
        core.Movement.Flip();
        core.Movement.SetVelocityX(stateData.chaseSpeed * core.Movement.FacingDirection);
      }
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
    isTouchingPlayer = entity.CheckTouchingPlayer();
  }
}
