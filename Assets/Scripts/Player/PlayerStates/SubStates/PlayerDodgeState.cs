using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerAbilityState {
  private bool canDodge;
  private Vector2 dodgeDirection;

  public PlayerDodgeState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    // player.InputHandler.UseDodgeInput();
    canDodge = false;
    dodgeDirection = new Vector2(xInput, yInput);
    Debug.Log(dodgeDirection);
    player.RB.gravityScale = 0.0f;
    player.BC.enabled = false;
  }

  public override void Exit() {
    base.Exit();

    player.RB.gravityScale = 1.0f;
    player.BC.enabled = true;
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isGrounded) {
      player.RB.gravityScale = 1.0f;
      player.BC.enabled = true;
      isAbilityDone = true;
    } else {
      core.Movement.SetVelocity(playerData.dodgeVelocity, new Vector2(xInput, yInput));
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public bool CanDodge() {
    return canDodge;
  }

  public void ResetCanDodge() {
    canDodge = true;
  }

  public override void AnimationFinishTrigger() {
    base.AnimationFinishTrigger();
    
    isAbilityDone = true;
  }
}