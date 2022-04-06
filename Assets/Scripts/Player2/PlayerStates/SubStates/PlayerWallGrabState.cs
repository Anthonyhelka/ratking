using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState {
  private Vector2 holdPosition;

  public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    holdPosition = player.transform.position;
    HoldPosition();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    HoldPosition();

    if (yInput > 0) {
      stateMachine.ChangeState(player.WallClimbState);
    } else if (yInput < 0 || !grabInput) {
      stateMachine.ChangeState(player.WallSlideState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  private void HoldPosition() {
    player.transform.position = holdPosition;
    player.SetVelocityX(0.0f);
    player.SetVelocityY(0.0f);
  }
}
