using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBananaSlippersIdleState : PlayerGroundedState {
  public PlayerBananaSlippersIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    if (playerData.selectedSpecial != PlayerData.Special.bananaSlippers) {
      stateMachine.ChangeState(player.IdleState);
    }
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (xInput != 0) {
      stateMachine.ChangeState(player.BananaSlippersMoveState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();

    if (Mathf.Abs(player.CurrentVelocity.x) > 0.01f) {
      player.SetVelocityX(player.CurrentVelocity.x * playerData.bananaSlippersSlipMultiplier);
    } else {
      player.SetVelocityX(0.0f);
    }
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
