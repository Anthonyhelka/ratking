using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRat_DeadState : DeadState {
  private BombRat bombRat;

  public BombRat_DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData, BombRat bombRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.bombRat = bombRat;
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