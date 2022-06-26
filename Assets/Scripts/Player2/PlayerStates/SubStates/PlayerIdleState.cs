using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState {
  public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (xInput != 0 && !isExitingState) {
      stateMachine.ChangeState(player.MoveState);
    } else if (Time.time >= startTime + playerData.sleepTime) {
      stateMachine.ChangeState(player.SleepState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();

    core.Movement.SetVelocityX(0.0f);
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
