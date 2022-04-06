using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState {
  public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    player.SetVelocityX(0.0f);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (xInput != 0) {
      stateMachine.ChangeState(player.MoveState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
