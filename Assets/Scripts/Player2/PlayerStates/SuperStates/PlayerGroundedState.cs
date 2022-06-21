using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState {
  protected int xInput;
  private bool jumpInput;
  private bool specialInput;
  private bool isGrounded;

  public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    player.JumpState.ResetAmountOfJumpsLeft();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    xInput = player.InputHandler.NormalizedInputX;
    jumpInput = player.InputHandler.JumpInput;
    specialInput = player.InputHandler.SpecialInput;

    if (specialInput) {
      if (playerData.selectedSpecial == PlayerData.Special.boomerang && player.BoomerangThrowState.CanThrowBoomerang()) {
        player.InputHandler.UseSpecialInput();
        stateMachine.ChangeState(player.BoomerangThrowState);
      }
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
