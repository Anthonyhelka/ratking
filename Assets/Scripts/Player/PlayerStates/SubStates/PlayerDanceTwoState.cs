using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDanceTwoState : PlayerAbilityState {
  public PlayerDanceTwoState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (xInput != 0 || jumpInput || dashInput || specialInput || primaryAttackInput || secondaryAttackInput || danceOneInput) {
      isAbilityDone = true;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();

    core.Movement.SetVelocityX(0.0f);
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}