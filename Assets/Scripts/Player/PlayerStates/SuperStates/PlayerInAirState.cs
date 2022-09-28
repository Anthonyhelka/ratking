using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState {
  // Input
  private int xInput;
  private bool jumpInput;
  private bool jumpInputStop;
  private bool dashInput;
  private bool crownArtInput;
  private bool specialInput;
  private bool primaryAttackInput;
  private bool secondaryAttackInput;
  private bool squeakInput;

  private bool isGrounded;
  private bool isHurt;
  private bool isJumping;
  private bool coyoteTime;
  private bool isDead;

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
    crownArtInput = player.InputHandler.CrownArtInput;
    specialInput = player.InputHandler.SpecialInput;
    primaryAttackInput = player.InputHandler.PrimaryAttackInput;
    secondaryAttackInput = player.InputHandler.SecondaryAttackInput;
    squeakInput = player.InputHandler.SqueakInput;

    CheckJumpMultiplier();

    if (isHurt && !player.isDead) {
      player.isHurt = false;
      stateMachine.ChangeState(player.HurtState);
    } else if (specialInput) {
      if (playerData.selectedSpecial == PlayerData.Special.boomerang && player.BoomerangThrowState.CanThrowBoomerang()) {
        player.InputHandler.UseSpecialInput();
        stateMachine.ChangeState(player.BoomerangThrowState);
      } else if (playerData.selectedSpecial == PlayerData.Special.glider) {
        player.InputHandler.UseSpecialInput();
        stateMachine.ChangeState(player.GlideState);
      } else if (playerData.selectedSpecial == PlayerData.Special.jetpack && player.JetpackChargeState.CanJetpackCharge()) {
        player.InputHandler.UseSpecialInput();
        stateMachine.ChangeState(player.JetpackChargeState);
      } else {
        player.InputHandler.UseSpecialInput();
      }
    } else if (primaryAttackInput && player.PrimaryAirAttackState.CanUse()) {
      stateMachine.ChangeState(player.PrimaryAirAttackState);
    } else if (secondaryAttackInput && player.SecondaryAirAttackState.CanUse()) {
      stateMachine.ChangeState(player.SecondaryAirAttackState);
    } else if (dashInput && player.DashState.CanDash()) {
      stateMachine.ChangeState(player.DashState);
    } else if (crownArtInput && player.CrownArtState.CanCrownArt()) {
      stateMachine.ChangeState(player.CrownArtState);
    } else if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f) {
      stateMachine.ChangeState(player.LandState);
    } else if (squeakInput && player.SqueakState.CanSqueak()) {
      player.InputHandler.UseSqueakInput();
      if (xInput == 0) {
        stateMachine.ChangeState(player.SqueakState);
      } else {
        player.SqueakState.lastUseTime = Time.time;
        player.PlaySqueak();
      }
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
