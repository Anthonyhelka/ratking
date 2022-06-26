using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState {
  public bool CanDash { get; private set; }
  private float lastDashTime;
  private Vector2 dashDirection;

  public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    CanDash = false;
    player.InputHandler.UseDashInput();
    dashDirection = Vector2.right * core.Movement.FacingDirection;
    player.RB.drag = playerData.drag;
    core.Movement.SetVelocity(playerData.dashVelocity, dashDirection);

    if (isGrounded) {
      player.Anim.SetBool("slide", true);
    }
  }

  public override void Exit() {
    base.Exit();

    player.Anim.SetBool("slide", false);
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.SetVelocity(playerData.dashVelocity, dashDirection);
    
    if (Time.time >= startTime + playerData.dashTime) {
      player.RB.drag = 0.0f;
      isAbilityDone = true;
      lastDashTime = Time.time;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public bool CheckIfCanDash() {
    return CanDash && Time.time >= lastDashTime + playerData.dashCooldown;
  }

  public void ResetCanDash() {
    CanDash = true;
  }
}