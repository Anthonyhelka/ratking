using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaRat_DeadState : DeadState {
  private NinjaRat ninjaRat;

  public NinjaRat_DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData, NinjaRat ninjaRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.ninjaRat = ninjaRat;
  }

  public override void Enter() {
    base.Enter();
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
  }
}