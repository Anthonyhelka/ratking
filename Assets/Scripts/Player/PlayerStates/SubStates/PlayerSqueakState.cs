using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSqueakState : PlayerAbilityState {
  public float lastUseTime;

  public PlayerSqueakState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    player.PlaySqueak();
  }

  public override void Exit() {
    base.Exit();

    lastUseTime = Time.time;
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (xInput != 0 || jumpInput || dashInput || specialInput || primaryAttackInput || secondaryAttackInput) {
      isAbilityDone = true;
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