using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrownTeleportThrowState : PlayerAbilityState {
  private GameObject crown;
  // private Crown crownScript;
  private bool willTeleport;

  public PlayerCrownTeleportThrowState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    willTeleport = false;
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isAnimationFinished) {
      isAbilityDone = true;
    }
    // if (canCancelAnimation && ((jumpInput && player.JumpState.CanJump()) || (dashInput && player.DashState.CanDash()))) {
    //   core.Movement.CheckIfShouldFlip(xInput);
    //   isAbilityDone = true;
    // } else if (isAnimationFinished) {
    //   isAbilityDone = true;
    // } else {
    //   core.Movement.SetVelocityX(playerData.movementVelocity * xInput);
    // }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public override void AnimationTrigger() {
    base.AnimationTrigger();

    // crown = GameObject.Instantiate(playerData.crown, player.crownPosition.position, player.crownPosition.rotation);
    // crownScript = crown.GetComponent<Crown>();
    // crownScript.ThrowCrown(playerData.crownVelocity, playerData.crownTravelDistance);
  }

  public bool WillTeleport() {
    return willTeleport;
  }

  public void CrownLanded() {
    willTeleport = true;
  } 
}