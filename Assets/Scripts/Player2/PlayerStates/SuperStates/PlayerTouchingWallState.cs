using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState {
  protected int xInput;
  protected int yInput;
  protected bool isGrounded;
  protected bool isTouchingWall;
  protected bool jumpInput;
  protected bool grabInput;
  protected bool isTouchingLedge;

  public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) { }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    xInput = player.inputHandler.normalizedInputX;
    yInput = player.inputHandler.normalizedInputY;
    jumpInput = player.inputHandler.jumpInput;
    grabInput = player.inputHandler.grabInput;

    if (jumpInput) {
      player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
      stateMachine.ChangeState(player.wallJumpState);
    } else if (isGrounded && !grabInput) {
      stateMachine.ChangeState(player.idleState);
    } else if (!isTouchingWall || (xInput != player.facingDirection && !grabInput)) {
      stateMachine.ChangeState(player.inAirState);
    } else if (isTouchingWall && !isTouchingLedge) {
      stateMachine.ChangeState(player.ledgeClimbState);
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

    if (isTouchingWall && !isTouchingLedge) {
      player.ledgeClimbState.SetDetectedPosition(player.transform.position);
    }
  }
}
