using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState : PlayerAbilityState {
  private bool jumpInputStop;
  private bool velcocityStopped;

  public PlayerDoubleJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    core.Movement.SetVelocityY(playerData.doubleJumpVelocity);
    player.Anim.SetBool("doubleJump", true);
    player.InAirState.SetIsJumping();
    player.JumpState.DecreaseAmountOfJumpsLeft();
    velcocityStopped = false;
  }

  public override void Exit() {
    base.Exit();

    player.Anim.SetBool("doubleJump", false);
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    jumpInputStop = player.InputHandler.JumpInputStop;
    CheckJumpMultiplier();

    if (isAnimationFinished || dashInput || specialInput || primaryAttackInput || secondaryAttackInput) {
      isAbilityDone = true;
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
  }

  private void CheckJumpMultiplier() {
    if (jumpInputStop && !velcocityStopped) {
      core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
      velcocityStopped = true;
    }
  }
}
