using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJetpackChargeState : PlayerAbilityState {
  public PlayerJetpackChargeState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    player.RB.gravityScale = 0.5f;
  }

  public override void Exit() {
    base.Exit();

    player.RB.gravityScale = 1.0f;
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.CheckIfShouldFlip(xInput);
    core.Movement.SetVelocityX(playerData.jetpackChargeXVelocity * xInput);
    core.Movement.SetVelocityY(playerData.jetpackChargeYVelocity);
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public override void AnimationFinishTrigger() {
    base.AnimationFinishTrigger();

    stateMachine.ChangeState(player.JetpackBlastState);
  }
}