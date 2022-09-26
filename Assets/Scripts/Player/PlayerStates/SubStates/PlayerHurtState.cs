using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerAbilityState {
  public float lastUseTime = -1.0f;
  private Vector2 knockbackDirection;
  public bool hurtFall;

  public PlayerHurtState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    player.health -= player.lastHitAttackDetails.damageAmount;

    if (player.health <= 0) {
      stateMachine.ChangeState(player.DeadState);
    } else {
      player.EnterCloakState.ResetCloakActive();
      knockbackDirection = player.lastHitAttackDetails.position - (Vector2)(player.transform.position);
      knockbackDirection.Normalize();
      hurtFall = false;
      player.Anim.SetBool("hurtFall", false);
    }
  }

  public override void Exit() {
    base.Exit();

    player.Anim.SetBool("hurtFall", false);
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (hurtFall) {
      player.Anim.SetBool("hurtFall", true);
      if (isGrounded || xInput != 0 || (jumpInput && player.JumpState.CanJump()) || (dashInput && player.DashState.CanDash()) || specialInput || primaryAttackInput || secondaryAttackInput) {
        player.Anim.SetBool("hurtFall", false);
        isAbilityDone = true;
      }
    } else {
      core.Movement.SetVelocity(playerData.hurtVelocity, new Vector2(-knockbackDirection.x, 1));
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

    hurtFall = true;
  }

  public bool CanUse() {
    return Time.time >= lastUseTime + playerData.invincibilityTimer;
  }
}