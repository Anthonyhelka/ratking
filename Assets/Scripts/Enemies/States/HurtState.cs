using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : State {
  protected D_HurtState stateData;
  protected bool isHurtTimeOver;

  public HurtState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_HurtState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }
  
  public override void Enter() {
    base.Enter();

    isHurtTimeOver = false;
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (Time.time >= startTime + stateData.hurtTime) {
      isHurtTimeOver = true;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
