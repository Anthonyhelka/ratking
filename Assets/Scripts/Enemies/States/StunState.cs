using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State {
  protected D_StunState stateData;

  protected bool isStunTimeOver;
  protected bool isPlayerInMinAggroRange;
  protected bool performCloseRangeAction;
  protected bool isTouchingPlayer;
  public float nextStunTime = 0.0f;

  public StunState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_StunState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }

  public override void Enter() {
    base.Enter();

    core.Movement.SetVelocityZero();
    isStunTimeOver = false;
  }

  public override void Exit() {
    base.Exit();

    nextStunTime = Time.time + stateData.stunCooldown;
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (Time.time >= startTime + stateData.stunTime) {
      isStunTimeOver = true;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
    
    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    isTouchingPlayer = entity.CheckTouchingPlayer();
  }
}