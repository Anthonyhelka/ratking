using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState {
  // Input
  private int xInput;
  private bool jumpInput;
  private bool jumpInputStop;
  private bool dashInput;
  private bool specialInput;

  private bool isGrounded;
  private bool isJumping;
  private bool coyoteTime;

  public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    CheckCoyoteTime();

    xInput = player.InputHandler.NormalizedInputX;
    jumpInput = player.InputHandler.JumpInput;
    jumpInputStop = player.InputHandler.JumpInputStop;
    dashInput = player.InputHandler.DashInput;
    specialInput = player.InputHandler.SpecialInput;

    CheckJumpMultiplier();

    if (specialInput) {
      if (playerData.selectedSpecial == PlayerData.Special.boomerang && player.BoomerangThrowState.CanThrowBoomerang()) {
        player.InputHandler.UseSpecialInput();
        stateMachine.ChangeState(player.BoomerangThrowState);
      } else if (playerData.selectedSpecial == PlayerData.Special.glider) {
        player.InputHandler.UseSpecialInput();
        stateMachine.ChangeState(player.GlideState);
      }
    } else if (player.InputHandler.AttackInputs[(int)CombatInputs.primary]) {
      stateMachine.ChangeState(player.PrimaryAttackState);
    } else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary]) {
      stateMachine.ChangeState(player.SecondaryAttackState);
    } else if (dashInput && player.DashState.CheckIfCanDash()) {
      stateMachine.ChangeState(player.DashState);
    } else if (isGrounded && player.CurrentVelocity.y < 0.01f) {
      stateMachine.ChangeState(player.LandState);
    } else if (jumpInput && player.JumpState.CanJump()) {
      if (coyoteTime) {
        player.InputHandler.UseJumpInput();
        stateMachine.ChangeState(player.JumpState);
      } else {
        player.InputHandler.UseJumpInput();
        stateMachine.ChangeState(player.DoubleJumpState);
      }
    } else {
      player.CheckIfShouldFlip(xInput);
      player.SetVelocityX(playerData.movementVelocity * xInput);
      player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
      player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isGrounded = player.CheckIfGrounded();
  }

  public void SetIsJumping() {
    isJumping = true;
  }

  private void CheckJumpMultiplier() {
    if (isJumping) {
      if (jumpInputStop) {
        player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
        isJumping = false;
      } else if (player.CurrentVelocity.y <= 0.0f) {
        isJumping = false;
      }
    }
  }

  public void StartCoyoteTime() {
    coyoteTime = true;
  }

  private void CheckCoyoteTime() {
    if (coyoteTime && Time.time > startTime + playerData.coyoteTime) {
      coyoteTime = false;
      player.JumpState.DecreaseAmountOfJumpsLeft();
    }
  }
}
