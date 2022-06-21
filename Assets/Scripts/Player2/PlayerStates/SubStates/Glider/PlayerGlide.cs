using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlideState : PlayerAbilityState {
  private int xInput;
  private bool specialInputStop;

  public PlayerGlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    player.SetVelocityY(0.0f);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    xInput = player.InputHandler.NormalizedInputX;
    specialInputStop = player.InputHandler.SpecialInputStop;

    if (isGrounded || specialInputStop) {
      isAbilityDone = true;
    } else {
      player.CheckIfShouldFlip(xInput);
      player.SetVelocityX(playerData.glideXVelocity * xInput);
      player.SetVelocityY(-playerData.glideYVelocity);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}