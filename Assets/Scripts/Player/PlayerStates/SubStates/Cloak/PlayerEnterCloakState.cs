using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterCloakState : PlayerAbilityState {
  private bool cloakActive;

  public PlayerEnterCloakState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.SetVelocityX(0.0f);
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public override void AnimationFinishTrigger() {
    base.AnimationTrigger();

    cloakActive = true;
    player.canBeDetected = false;
    player.SR.color = playerData.cloakColor;
    isAbilityDone = true;
  }

  public void ResetCloakActive() {
    cloakActive = false;
    player.canBeDetected = true;
    player.SR.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
  }

  public bool CloakActive() {
    return cloakActive;
  }
}