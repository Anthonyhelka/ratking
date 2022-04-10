using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlungeFormThrowState : PlayerAbilityState {
  private GameObject plungeform;
  private PlungeForm plungeformScript;
  private float plungeformThrowCooldown;

  public PlayerPlungeFormThrowState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    plungeform = GameObject.Instantiate(playerData.plungeform, player.plungeformPosition.position, player.plungeformPosition.rotation);
    plungeformScript = plungeform.GetComponent<PlungeForm>();
    plungeformScript.FirePlungeform(playerData.plungeformSpeed, playerData.plungeformTravelDistance, playerData.plungeformDamage, player.FacingDirection);

    plungeformThrowCooldown = Time.time + playerData.plungeformCooldown;

    stateMachine.ChangeState(player.IdleState);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public override void AnimationTrigger() {
    base.AnimationTrigger();
  }

  public bool CanThrowPlungeform() {
    return Time.time > plungeformThrowCooldown;
  }
}