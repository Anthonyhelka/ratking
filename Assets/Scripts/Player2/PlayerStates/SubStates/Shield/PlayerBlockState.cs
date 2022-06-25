using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerAbilityState {
  private bool specialInputStop;
  private bool isBlocking;

  public PlayerBlockState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    core.Movement.SetVelocityX(0.0f);
    player.Anim.SetBool("startingBlock", true);
  }

  public override void Exit() {
    base.Exit();

    isBlocking = false;
    player.Anim.SetBool("startingBlock", false);
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    xInput = player.InputHandler.NormalizedInputX;
    specialInputStop = player.InputHandler.SpecialInputStop;

    if ((!isGrounded || specialInputStop) && isBlocking) {
      isAbilityDone = true;
    } else {
      core.Movement.CheckIfShouldFlip(xInput);
      core.Movement.SetVelocityX(0.0f);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public override void AnimationTrigger() {
    base.AnimationTrigger(); 

    isBlocking = true;
    player.Anim.SetBool("startingBlock", false);
  }

}