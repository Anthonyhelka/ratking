using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAirAttackState : PlayerAbilityState {
  public AttackDetails attackDetails;
  private float lastUseTime;

  public PlayerPrimaryAirAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();

    lastUseTime = Time.time;
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.CheckIfShouldFlip(xInput);
    core.Movement.SetVelocityX(playerData.movementVelocity * xInput);

    if ((jumpInput && player.JumpState.CanJump()) || (dashInput && player.DashState.CanDash())) {
      isAbilityDone = true;
    } else if (isGrounded) {
      stateMachine.ChangeState(player.RollState);
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

    Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(player.transform.position, playerData.primaryAirAttackRadius, player.whatIsDamageable);
    foreach (Collider2D collider in detectedObjects) {
      IDamageable damageable = collider.GetComponentInParent<IDamageable>();
      if (damageable != null) {
        attackDetails.position = player.transform.position;
        attackDetails.damageAmount = (int)playerData.primaryAirAttackDamage;
        damageable.Damage(attackDetails);
        stateMachine.ChangeState(player.BounceState);
      }
    }
  }

  public override void DrawGizmos() {
    base.DrawGizmos();

    Gizmos.DrawWireSphere(player.transform.position, playerData.primaryAirAttackRadius);
  }

  public bool CanUse() {
    return Time.time >= lastUseTime + playerData.primaryAirAttackCooldown;
  }
}