using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState {
  // Inputs
  protected int xInput;
  private bool jumpInput;
  private bool dashInput;
  private bool specialInput;
  private bool primaryAttackInput;
  private bool secondaryAttackInput;

  private bool isGrounded;
  private bool isHurt;

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
    primaryAttackInput = player.InputHandler.PrimaryAttackInput;
    secondaryAttackInput = player.InputHandler.SecondaryAttackInput;
    
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
      } else {
        player.InputHandler.UseSpecialInput();
      }
    } else if (primaryAttackInput && player.PrimaryGroundAttackState.CanUse()) {
      stateMachine.ChangeState(player.PrimaryGroundAttackState);
    } else if (secondaryAttackInput && player.SecondaryGroundAttackState.CanUse()) {
      stateMachine.ChangeState(player.SecondaryGroundAttackState);
    } else if (dashInput && player.DashState.CanDash()) {
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

    isGrounded = core.CollisionSenses.Grounded;
    isHurt = player.isHurt;
  }
}
