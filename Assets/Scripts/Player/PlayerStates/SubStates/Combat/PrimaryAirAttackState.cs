using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAirAttackState : PlayerAbilityState {
  public AttackDetails attackDetails;
  private bool canUse = true;

  public PlayerPrimaryAirAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    player.EnterCloakState.ResetCloakActive();
    canUse = false;
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.CheckIfShouldFlip(xInput);
    core.Movement.SetVelocityX(playerData.movementVelocity * xInput);

    if ((jumpInput && player.JumpState.CanJump()) || (dashInput && player.DashState.CanDash()) || (secondaryAttackInput && player.SecondaryAirAttackState.CanUse())) {
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

      IKnockbackable knockbackable = collider.GetComponentInParent<IKnockbackable>();
      if (knockbackable != null) {
        knockbackable.Knockback(playerData.primaryAirAttackKnockbackAngle, playerData.primaryAirAttackKnockbackStength, collider.transform.position.x > player.transform.position.x ? 1 : -1);
      }
    }
  }

  public override void DrawGizmos() {
    base.DrawGizmos();

    Gizmos.DrawWireSphere(player.transform.position, playerData.primaryAirAttackRadius);
  }

  public void ResetCanUse() {
    canUse = true;
  }

  public bool CanUse() {
    return canUse;
  }
}