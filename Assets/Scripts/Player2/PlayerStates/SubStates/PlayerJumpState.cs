using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState {
  private int amountOfJumpsLeft;

  public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
    amountOfJumpsLeft = playerData.amountOfJumps;
  }

  public override void Enter() {
    base.Enter();

    player.inputHandler.UseJumpInput();
    amountOfJumpsLeft--;
    player.SetVelocityY(playerData.jumpVelocity);
    player.inAirState.SetIsJumping();
    isAbilityDone = true;
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public bool CanJump() {
    return amountOfJumpsLeft > 0 ? true : false;
  }

  public void DecreaseAmountOfJumpsLeft() {
    amountOfJumpsLeft--;
  }

  public void ResetAmountOfJumpsLeft() {
    amountOfJumpsLeft = playerData.amountOfJumps;
  }
}