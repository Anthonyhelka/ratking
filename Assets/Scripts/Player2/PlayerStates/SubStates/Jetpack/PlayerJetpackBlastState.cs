using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJetpackBlastState : PlayerAbilityState {
  private bool endBlast;

  public PlayerJetpackBlastState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    endBlast = false;
    player.RB.drag = playerData.drag;
  }

  public override void Exit() {
    base.Exit();

  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (endBlast) {
      player.RB.drag = 0.0f;
      if (isGrounded || jumpInput || dashInput || specialInput || primaryAttackInput || secondaryAttackInput) {
        isAbilityDone = true;
      }
    } else {
      core.Movement.SetVelocity(playerData.jetpackBlastVelocity, new Vector2(playerData.jetpackBlastDirection.x * core.Movement.FacingDirection, playerData.jetpackBlastDirection.y));
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

    endBlast = true;
  }
}