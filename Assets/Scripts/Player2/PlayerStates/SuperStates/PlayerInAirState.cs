using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState {
  // Input
  protected int xInput;
  protected bool jumpInput;
  protected bool jumpInputStop;
  protected bool dashInput;
  protected bool grabInput;
  // Checks
  protected bool isGrounded;
  protected bool isTouchingWall;
  protected bool isTouchingWallBack;
  protected bool oldIsTouchingWall;
  protected bool oldIsTouchingWallBack;
  protected bool isTouchingLedge;
  // Other
  protected bool coyoteTime;
  protected bool wallJumpCoyoteTime;
  protected bool isJumping;
  protected float startWallJumpCoyoteTime;


  public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) { }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();

    oldIsTouchingWall = false;
    oldIsTouchingWallBack = false;
    isTouchingWall = false;
    isTouchingWallBack = false;
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    xInput = player.inputHandler.normalizedInputX;
    jumpInput = player.inputHandler.jumpInput;
    jumpInputStop = player.inputHandler.jumpInputStop;
    dashInput = player.inputHandler.dashInput;
    grabInput = player.inputHandler.grabInput;

    CheckJumpMultiplier();
    CheckCoyoteTime();
    CheckWallJumpCoyoteTime();

    if (isGrounded && player.currentVelocity.y < 0.01f) {
      stateMachine.ChangeState(player.landState);
    } else if (isTouchingWall && !isTouchingLedge && !isGrounded) {
      stateMachine.ChangeState(player.ledgeClimbState);
    } else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime)) {
      StopWallJumpCoyoteTime();
      isTouchingWall = player.CheckIfTouchingWall();
      player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
      stateMachine.ChangeState(player.wallJumpState);
    } else if (jumpInput && player.jumpState.CanJump()) {
      coyoteTime = false;
      stateMachine.ChangeState(player.jumpState);
    } else if (isTouchingWall && isTouchingLedge && grabInput) {
      stateMachine.ChangeState(player.wallGrabState);
    } else if (isTouchingWall && xInput == player.facingDirection && player.currentVelocity.y <= 0.0f) {
      stateMachine.ChangeState(player.wallSlideState);
    } else if (dashInput && player.dashState.CanDash()) {
      stateMachine.ChangeState(player.dashState);
    } else {
      player.CheckIfShouldFlip(xInput);
      player.SetVelocityX(playerData.movementVelocity * xInput);
      player.animator.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));
      player.animator.SetFloat("yVelocity", player.currentVelocity.y);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isGrounded = player.CheckIfGrounded();
    oldIsTouchingWall = isTouchingWall;
    oldIsTouchingWallBack = isTouchingWallBack;
    isTouchingWall = player.CheckIfTouchingWall();
    isTouchingWallBack = player.CheckIfTouchingWallBack();
    isTouchingLedge = player.CheckIfTouchingLedge();

    if (isTouchingWall && !isTouchingLedge) {
      player.ledgeClimbState.SetDetectedPosition(player.transform.position);
    }

    if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack)) {
      StartWallJumpCoyoteTime();
    }
  }

  public void SetIsJumping() {
    isJumping = true;
  }

  private void CheckJumpMultiplier() {
    if (isJumping) {
      if (jumpInputStop) {
        player.SetVelocityY(player.currentVelocity.y * playerData.variableJumpHeightMultiplier);
        isJumping = false;
      } else if (player.currentVelocity.y <= 0.0f) {
        isJumping = false;
      }
    }
  }

  private void CheckCoyoteTime() {
    if (coyoteTime && Time.time > startTime + playerData.coyoteTime) {
      coyoteTime = false;
      player.jumpState.DecreaseAmountOfJumpsLeft();
    } 
  }

  public void StartCoyoteTime() {
    coyoteTime = true;
  }

  private void CheckWallJumpCoyoteTime() {
    if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime) {
      wallJumpCoyoteTime = false;
      player.jumpState.DecreaseAmountOfJumpsLeft();
    } 
  }

  public void StartWallJumpCoyoteTime() {
    wallJumpCoyoteTime = true;
    startWallJumpCoyoteTime = Time.time;
  }

  public void StopWallJumpCoyoteTime() {
    wallJumpCoyoteTime = false;
  }
}