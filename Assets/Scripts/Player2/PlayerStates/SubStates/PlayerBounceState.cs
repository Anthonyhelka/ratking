using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounceState : PlayerAbilityState {
  public PlayerBounceState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    core.Movement.SetVelocityY(playerData.bounceVelocity);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isGrounded) {
      isAbilityDone = true;
    } else {
      core.Movement.CheckIfShouldFlip(xInput);
      core.Movement.SetVelocityX(playerData.movementVelocity * xInput);
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

    isAbilityDone = true;
  }
}