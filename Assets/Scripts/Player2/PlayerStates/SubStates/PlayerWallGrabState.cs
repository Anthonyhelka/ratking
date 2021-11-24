using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState {
  protected Vector2 holdPosition;

  public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) { }

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

    if (!isExitingState) {
      HoldPosition();
      if (yInput > 0.0f) {
        stateMachine.ChangeState(player.wallClimbState);
      } else if (!isGrounded && (yInput < 0.0f || !grabInput)) {
        stateMachine.ChangeState(player.wallSlideState);
      }
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
    player.SetVelocityZero();
  }
}