using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave_DeadState : DeadState {
  private Shockwave shockwave;

  public Shockwave_DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData, Shockwave shockwave) : base(entity, stateMachine, animationBoolName, stateData) {
    this.shockwave = shockwave;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    entity.gameObject.SetActive(false);
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}