using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive_DeadState : DeadState {
  private Hive hive;

  public Hive_DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData, Hive hive) : base(entity, stateMachine, animationBoolName, stateData) {
    this.hive = hive;
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