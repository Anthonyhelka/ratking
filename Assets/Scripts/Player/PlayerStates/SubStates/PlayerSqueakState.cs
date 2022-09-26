using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSqueakState : PlayerAbilityState {
  private float lastUseTime;

  public PlayerSqueakState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    switch(Random.Range(0, 3)) {
      case 0:
        player.squeakOneAudio.Play();
        break;
      case 1:
        player.squeakTwoAudio.Play();
        break;
      case 2:
        player.squeakThreeAudio.Play();
        break;
    }
  }

  public override void Exit() {
    base.Exit();

    lastUseTime = Time.time;
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (jumpInput || dashInput || specialInput || primaryAttackInput || secondaryAttackInput) {
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

  public bool CanSqueak() {
    return Time.time >= lastUseTime + playerData.squeakCooldown;
  }
}