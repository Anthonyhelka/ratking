using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState {
  // Inputs
  protected int xInput;
  private bool jumpInput;
  private bool dashInput;
  private bool crownArtInput;
  private bool specialInput;
  private bool primaryAttackInput;
  private bool secondaryAttackInput;
  private bool danceInput;

  private bool isGrounded;
  private bool isHurt;

  public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    player.JumpState.ResetAmountOfJumpsLeft();
    player.DashState.ResetCanDash();
    player.CrownArtState.ResetCanCrownArt();
    player.JetpackChargeState.ResetCanJetpackCharge();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    xInput = player.InputHandler.NormalizedInputX;
    jumpInput = player.InputHandler.JumpInput;
    dashInput = player.InputHandler.DashInput;
    crownArtInput = player.InputHandler.CrownArtInput;
    specialInput = player.InputHandler.SpecialInput;
    primaryAttackInput = player.InputHandler.PrimaryAttackInput;
    secondaryAttackInput = player.InputHandler.SecondaryAttackInput;
    danceInput = player.InputHandler.DanceInput;

    if (isHurt) {
      player.isHurt = false;
      stateMachine.ChangeState(player.HurtState);
    } else if (specialInput) {
      if (playerData.selectedSpecial == PlayerData.Special.boomerang && player.BoomerangThrowState.CanThrowBoomerang()) {
        player.InputHandler.UseSpecialInput();
        stateMachine.ChangeState(player.BoomerangThrowState);
      } else if (playerData.selectedSpecial == PlayerData.Special.shield) {
        player.InputHandler.UseSpecialInput();
        stateMachine.ChangeState(player.StartBlockState);
      } else if (playerData.selectedSpecial == PlayerData.Special.cloak) {
        player.InputHandler.UseSpecialInput();
        if (!player.EnterCloakState.CloakActive()) {
          stateMachine.ChangeState(player.EnterCloakState);
        } else {
          stateMachine.ChangeState(player.ExitCloakState);
        }
      } else {
        player.InputHandler.UseSpecialInput();
      }
    } else if (primaryAttackInput && player.PrimaryGroundAttackState.CanUse()) {
      stateMachine.ChangeState(player.PrimaryGroundAttackState);
    } else if (secondaryAttackInput && player.SecondaryGroundAttackState.CanUse()) {
      stateMachine.ChangeState(player.SecondaryGroundAttackState);
    } else if (dashInput && player.DashState.CanDash()) {
      stateMachine.ChangeState(player.DashState);
    } else if (crownArtInput && player.CrownArtState.CanCrownArt()) {
      stateMachine.ChangeState(player.CrownArtState);
    } else if (jumpInput && player.JumpState.CanJump()) {
      player.InputHandler.UseJumpInput();
      stateMachine.ChangeState(player.JumpState);
    } else if (danceInput) {
      player.InputHandler.UseDanceInput();
      stateMachine.ChangeState(player.DanceState);
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

    isGrounded = core.CollisionSenses.Grounded;
    isHurt = player.isHurt;
  }
}
