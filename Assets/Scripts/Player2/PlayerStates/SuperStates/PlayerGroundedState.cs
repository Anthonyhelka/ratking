using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState {
  protected int xInput;
  protected bool jumpInput;
  protected bool dashInput;
  protected bool grabInput;
  protected bool isGrounded;
  protected bool isTouchingWall;
  protected bool isTouchingLedge;
  public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) { }

  public override void Enter() {
    base.Enter();

    player.jumpState.ResetAmountOfJumpsLeft();
    player.dashState.ResetDash();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    xInput = player.inputHandler.normalizedInputX;
    jumpInput = player.inputHandler.jumpInput;
    dashInput = player.inputHandler.dashInput;
    grabInput = player.inputHandler.grabInput;

    if (jumpInput && player.jumpState.CanJump()) {
      stateMachine.ChangeState(player.jumpState);
    } else if (!isGrounded) {
      player.inAirState.StartCoyoteTime();
      stateMachine.ChangeState(player.inAirState);
    } else if (isTouchingWall && isTouchingLedge && grabInput) {
      stateMachine.ChangeState(player.wallGrabState);
    } else if (dashInput && player.dashState.CanDash()) {
      stateMachine.ChangeState(player.dashState);
    } 
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isGrounded = player.CheckIfGrounded();
    isTouchingWall = player.CheckIfTouchingWall();
    isTouchingLedge = player.CheckIfTouchingLedge();
  }
}
