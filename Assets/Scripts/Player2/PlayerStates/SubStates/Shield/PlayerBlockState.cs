using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerAbilityState {
  private bool specialInputStop;

  public PlayerBlockState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    core.Movement.SetVelocityX(0.0f);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    xInput = player.InputHandler.NormalizedInputX;
    specialInputStop = player.InputHandler.SpecialInputStop;

    if (specialInputStop) {
      stateMachine.ChangeState(player.EndBlockState);
    } else if (!isGrounded) {
      isAbilityDone = true;
    } else {
      core.Movement.CheckIfShouldFlip(xInput);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}