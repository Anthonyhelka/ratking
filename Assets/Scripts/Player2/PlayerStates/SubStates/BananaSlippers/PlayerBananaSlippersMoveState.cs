using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBananaSlippersMoveState : PlayerGroundedState {
  public PlayerBananaSlippersMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    if (playerData.selectedSpecial != PlayerData.Special.bananaSlippers) {
      stateMachine.ChangeState(player.MoveState);
    }
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (xInput == 0) {
      stateMachine.ChangeState(player.BananaSlippersIdleState);
    } else {
      player.CheckIfShouldFlip(xInput);
      player.SetVelocityX(playerData.bananaSlippersMovementVelocity * xInput);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}