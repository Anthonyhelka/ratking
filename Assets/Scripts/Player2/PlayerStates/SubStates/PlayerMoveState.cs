using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState {
  public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    if (playerData.selectedSpecial == PlayerData.Special.bananaSlippers) {
      stateMachine.ChangeState(player.BananaSlippersMoveState);
    }
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (xInput == 0) {
      stateMachine.ChangeState(player.IdleState);
    } else {
      player.CheckIfShouldFlip(xInput);
      player.SetVelocityX(playerData.movementVelocity * xInput);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
