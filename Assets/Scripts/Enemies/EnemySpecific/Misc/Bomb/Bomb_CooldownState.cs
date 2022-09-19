using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_CooldownState : CooldownState {
  private Bomb bomb;

  public Bomb_CooldownState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_CooldownState stateData, Bomb bomb) : base(entity, stateMachine, animationBoolName, stateData) {
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

    if (isCooldownTimeOver) {
      stateMachine.ChangeState(bomb.meleeAttackState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
