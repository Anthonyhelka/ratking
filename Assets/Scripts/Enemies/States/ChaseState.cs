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
    
    entity.SetVelocity(stateData.chaseSpeed);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (entity.facingDirection != (entity.lastPlayerDetectedPosition.x <= entity.alive.transform.position.x ? -1 : 1) && !willTurn) {
      willTurn = true;
      turnStartTime = Time.time;
    }

    if (willTurn) {
      if (Time.time > turnStartTime + stateData.turnDelayTime) {
        willTurn = false;
        entity.Flip();
        entity.SetVelocity(stateData.chaseSpeed);
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
