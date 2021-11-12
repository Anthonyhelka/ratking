using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedRat_DeadState : DeadState {
  private InfectedRat infectedRat;

  public InfectedRat_DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData, InfectedRat infectedRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.infectedRat = infectedRat;
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