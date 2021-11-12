using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongWalker_DeadState : DeadState {
  private LongWalker longWalker;

  public LongWalker_DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData, LongWalker longWalker) : base(entity, stateMachine, animationBoolName, stateData) {
    this.longWalker = longWalker;
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