using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState {
  // Inputs
  protected int xInput;
  private bool jumpInput;
  private bool dashInput;
  private bool specialInput;

  private bool isGrounded;

  public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    player.JumpState.ResetAmountOfJumpsLeft();
    player.DashState.ResetCanDash();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    xInput = player.InputHandler.NormalizedInputX;
    jumpInput = player.InputHandler.JumpInput;
    dashInput = player.InputHandler.DashInput;
    specialInput = player.InputHandler.SpecialInput;

    if (specialInput && playerData.selectedSpecial == PlayerData.Special.boomerang && player.BoomerangThrowState.CanThrowBoomerang()) {
      if (playerData.selectedSpecial == PlayerData.Special.boomerang && player.BoomerangThrowState.CanThrowBoomerang()) {
        player.InputHandler.UseSpecialInput();
        stateMachine.ChangeState(player.BoomerangThrowState);
      }
    } else if (dashInput && player.DashState.CheckIfCanDash()) {
      stateMachine.ChangeState(player.DashState);
    } else if (jumpInput && player.JumpState.CanJump()) {
      player.InputHandler.UseJumpInput();
      stateMachine.ChangeState(player.JumpState);
    } else if (!isGrounded) {
      player.InAirState.StartCoyoteTime();
      stateMachine.ChangeState(player.InAirState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isGrounded = player.CheckIfGrounded();
  }
}
