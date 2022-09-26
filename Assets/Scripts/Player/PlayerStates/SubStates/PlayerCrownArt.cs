using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrownArtState : PlayerAbilityState {
  private int xInput;
  private bool canCrownArt;
  
  public PlayerCrownArtState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    player.InputHandler.UseCrownArtInput();
    canCrownArt = false;
    player.EnterCloakState.ResetCloakActive();

    if (core.Movement.CurrentVelocity.x > playerData.movementVelocity) {
      core.Movement.SetVelocityX(playerData.movementVelocity);
    } else if (core.Movement.CurrentVelocity.x < -playerData.movementVelocity) {
      core.Movement.SetVelocityX(-playerData.movementVelocity);
    }
    core.Movement.SetVelocityY(playerData.crownArtYVelocity);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    xInput = player.InputHandler.NormalizedInputX;

    if (isGrounded && startTime + playerData.minimumCrownArtDuration < Time.time) {
      isAbilityDone = true;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public override void AnimationFinishTrigger() {
    base.AnimationFinishTrigger();

    if (!player.JumpState.CanJump()) {
      player.JumpState.IncreaseAmountOfJumpsLeft();
    }
    if (!player.DashState.CanDash()) {
      player.DashState.ResetCanDash();
    }
    if (!player.DodgeState.CanDodge()) {
      player.DodgeState.ResetCanDodge();
    }

    isAbilityDone = true;
  }

  public bool CanCrownArt() {
    return canCrownArt;
  }

  public void ResetCanCrownArt() {
    canCrownArt = true;
  }
}