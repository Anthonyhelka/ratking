using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondaryGroundAttackState : PlayerAbilityState {
  public AttackDetails attackDetails;
  private float lastUseTime;

  public PlayerSecondaryGroundAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    core.Movement.SetVelocityX(0.0f);
  }

  public override void Exit() {
    base.Exit();

    lastUseTime = Time.time;
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.SetVelocityX(0.0f);

    if ((jumpInput && player.JumpState.CanJump()) || (dashInput && player.DashState.CanDash())) {
      isAbilityDone = true;
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

    Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(player.transform.position, playerData.secondaryGroundAttackRadius, player.whatIsDamageable);
    foreach (Collider2D collider in detectedObjects) {
      IDamageable damageable = collider.GetComponentInParent<IDamageable>();
      if (damageable != null) {
        attackDetails.position = player.transform.position;
        attackDetails.damageAmount = (int)playerData.secondaryGroundAttackDamage;
        damageable.Damage(attackDetails);
      }

      IKnockbackable knockbackable = collider.GetComponentInParent<IKnockbackable>();
      if (knockbackable != null) {
        knockbackable.Knockback(playerData.secondaryGroundAttackKnockbackAngle, playerData.secondaryGroundAttackKnockbackStength, core.Movement.FacingDirection);
      }
    }
  }
  
  public override void AnimationFinishTrigger() {
    base.AnimationFinishTrigger(); 

    isAbilityDone = true;
  }

  public override void DrawGizmos() {
    base.DrawGizmos();

    Gizmos.DrawWireSphere(player.transform.position, playerData.secondaryGroundAttackRadius);
  }

  public bool CanUse() {
    return Time.time >= lastUseTime + playerData.secondaryGroundAttackCooldown;
  }
}