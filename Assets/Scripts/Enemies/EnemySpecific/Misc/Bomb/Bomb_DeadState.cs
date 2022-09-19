using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_DeadState : DeadState {
  private Bomb bomb;

  public Bomb_DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData, Bomb bomb) : base(entity, stateMachine, animationBoolName, stateData) {
    this.bomb = bomb;
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