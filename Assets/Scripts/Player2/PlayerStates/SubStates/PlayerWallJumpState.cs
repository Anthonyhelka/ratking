using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState {
  private int wallJumpDirection;

  public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) { }

  public override void Enter() {
    base.Enter();

    player.inputHandler.UseJumpInput();
    player.jumpState.ResetAmountOfJumpsLeft();
    player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
    player.CheckIfShouldFlip(wallJumpDirection);
    player.jumpState.DecreaseAmountOfJumpsLeft();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (!isExitingState) {
      player.animator.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));
      player.animator.SetFloat("yVelocity", player.currentVelocity.y);
      if (Time.time >= startTime + playerData.wallJumpTime) {
        isAbilityDone = true;
      }
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public void DetermineWallJumpDirection(bool isTouchingWall) {
    if (isTouchingWall) {
      wallJumpDirection = -player.facingDirection;
    } else {
      wallJumpDirection = player.facingDirection;
    }
  }
}