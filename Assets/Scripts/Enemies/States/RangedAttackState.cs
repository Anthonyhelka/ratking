using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : AttackState {
  protected D_RangedAttackState stateData;

  protected bool isTouchingPlayer;
  protected GameObject projectile;
  protected Projectile projectileScript;
  
  public RangedAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_RangedAttackState stateData) : base(entity, stateMachine, animationBoolName, attackPosition) {
    this.stateData = stateData;
  }

  public override void Enter() {
    base.Enter();

    if (core.Movement.FacingDirection != (entity.lastPlayerDetectedPosition.x <= entity.transform.position.x ? -1 : 1)) {
      core.Movement.Flip();
    }
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

    isTouchingPlayer = entity.CheckTouchingPlayer();
  }

  public override void TriggerAttack() {
    base.TriggerAttack();

    projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
    projectileScript = projectile.GetComponent<Projectile>();
    projectileScript.FireProjectile((attackPosition.position - entity.transform.position).normalized * stateData.projectileSpeed, stateData.projectTravelDistance, stateData.projectileDamage, entity.entityData.type);
  }
}