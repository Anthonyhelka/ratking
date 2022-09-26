using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoomerangThrowState : PlayerAbilityState {
  private GameObject boomerang;
  private Boomerang boomerangScript;
  private bool canThrowBoomerang = true;
  private bool canCancelAnimation;

  public PlayerBoomerangThrowState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    canThrowBoomerang = false;
    canCancelAnimation = false;
    player.EnterCloakState.ResetCloakActive();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (canCancelAnimation && ((jumpInput && player.JumpState.CanJump()) || (dashInput && player.DashState.CanDash()))) {
      core.Movement.CheckIfShouldFlip(xInput);
      isAbilityDone = true;
    } else if (isAnimationFinished) {
      isAbilityDone = true;
    } else {
      core.Movement.SetVelocityX(playerData.movementVelocity * xInput);
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

    boomerang = GameObject.Instantiate(playerData.boomerang, player.boomerangPosition.position, player.boomerangPosition.rotation);
    boomerangScript = boomerang.GetComponent<Boomerang>();
    boomerangScript.FireBoomerang(playerData.boomerangVelocity, playerData.boomerangTravelDistance, playerData.boomerangDamage);
    canCancelAnimation = true;
  }

  public void CaughtBoomerang() {
    canThrowBoomerang = true;
  }

  public bool CanThrowBoomerang() {
    return canThrowBoomerang;
  }
}