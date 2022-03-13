using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {
  protected D_IdleState stateData;

  protected bool flipAfterIdle;
  protected float idleTime;
  protected bool isIdleTimeOver;

  protected bool isPlayerInMinAggroRange;
  protected bool isTouchingPlayer;
  
  public IdleState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_IdleState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }

  public override void Enter() {
    base.Enter();

    entity.SetVelocity(0.0f);
    isIdleTimeOver = false;
    SetRandomIdleTime();
  }

  public override void Exit() {
    base.Exit();

    if (flipAfterIdle) {
      entity.Flip();
    }
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    entity.SetVelocity(0.0f);

    if (Time.time >= startTime + idleTime) {
      isIdleTimeOver = true;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    isTouchingPlayer = entity.CheckTouchingPlayer();
  }

  public void SetFlipAfterIdle(bool flip) {
    flipAfterIdle = flip;
  }

  private void SetRandomIdleTime() {
    idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
  }
}
