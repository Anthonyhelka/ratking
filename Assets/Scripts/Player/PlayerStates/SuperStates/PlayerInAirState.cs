using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState {
  // Input
  private int xInput;
  private bool jumpInput;
  private bool jumpInputStop;
  private bool dashInput;
  private bool dodgeInput;
  private bool specialInput;
  private bool primaryAttackInput;
  private bool secondaryAttackInput;

  private bool isGrounded;
  private bool isHurt;
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
    dodgeInput = player.InputHandler.DodgeInput;
    specialInput = player.InputHandler.SpecialInput;
    primaryAttackInput = player.InputHandler.PrimaryAttackInput;
    secondaryAttackInput = player.InputHandler.SecondaryAttackInput;

    CheckJumpMultiplier();

    if (isHurt) {
      player.isHurt = false;
      stateMachine.ChangeState(player.HurtState);
    } else if (specialInput) {
      if (playerData.selectedSpecial == PlayerData.Special.boomerang && player.BoomerangThrowState.CanThrowBoomerang()) {
        player.InputHandler.UseSpecialInput();
        stateMachine.ChangeState(player.BoomerangThrowState);
      } else if (playerData.selectedSpecial == PlayerData.Special.glider) {
        player.InputHandler.UseSpecialInput();
        stateMachine.ChangeState(player.GlideState);
      } else if (playerData.selectedSpecial == PlayerData.Special.jetpack) {
        player.InputHandler.UseSpecialInput();
        stateMachine.ChangeState(player.JetpackChargeState);
      } else {
        player.InputHandler.UseSpecialInput();
      }
    } else if (primaryAttackInput && player.PrimaryGroundAttackState.CanUse()) {
      stateMachine.ChangeState(player.PrimaryAirAttackState);
    } else if (secondaryAttackInput && player.SecondaryAirAttackState.CanUse()) {
      stateMachine.ChangeState(player.SecondaryAirAttackState);
    } else if (dashInput && player.DashState.CanDash()) {
      stateMachine.ChangeState(player.DashState);
    } else if (dodgeInput && player.DodgeState.CanDodge()) {
      stateMachine.ChangeState(player.DodgeState);
    } else if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f) {
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
      core.Movement.CheckIfShouldFlip(xInput);
      core.Movement.SetVelocityX(playerData.movementVelocity * xInput);
      player.Anim.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));
      player.Anim.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isGrounded = core.CollisionSenses.Grounded;
    isHurt = player.isHurt;
  }

  public void SetIsJumping() {
    isJumping = true;
  }

  private void CheckJumpMultiplier() {
    if (isJumping) {
      if (jumpInputStop) {
        core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
        isJumping = false;
      } else if (core.Movement.CurrentVelocity.y <= 0.0f) {
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
