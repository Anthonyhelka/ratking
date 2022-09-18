using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryGroundAttackState : PlayerAbilityState {
  public int attackCounter;
  public AttackDetails attackDetails;
  private float lastUseTime;

  public PlayerPrimaryGroundAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    attackCounter = 0;
    core.Movement.SetVelocityX(0.0f);
  }

  public override void Exit() {
    base.Exit();

    lastUseTime = Time.time;
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

    core.Movement.CheckIfShouldFlip(xInput);
    core.Movement.SetVelocityX(playerData.primaryGroundAttackMovement[attackCounter] * core.Movement.FacingDirection);

    Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(player.attackCheck.position, playerData.primaryGroundAttackRadius[attackCounter], player.whatIsDamageable);
    foreach (Collider2D collider in detectedObjects) {
      IDamageable damageable = collider.GetComponentInParent<IDamageable>();
      if (damageable != null) {
        attackDetails.position = player.transform.position;
        attackDetails.damageAmount = (int)playerData.primaryGroundAttackDamage[attackCounter];
        damageable.Damage(attackDetails);
      }

      IKnockbackable knockbackable = collider.GetComponentInParent<IKnockbackable>();
      if (knockbackable != null) {
        knockbackable.Knockback(playerData.primaryGroundAttackKnockbackAngle[attackCounter], playerData.primaryGroundAttackKnockbackStength[attackCounter], core.Movement.FacingDirection);
      }
    }

    attackCounter++;
  }
  
  public override void AnimationFinishTrigger() {
    base.AnimationFinishTrigger(); 

    isAbilityDone = true;
  }

  public override void DrawGizmos() {
    base.DrawGizmos();

    Gizmos.DrawWireSphere(player.attackCheck.position, playerData.primaryGroundAttackRadius[attackCounter]);
  }

  public bool CanUse() {
    return Time.time >= lastUseTime + playerData.primaryGroundAttackCooldown;
  }
}