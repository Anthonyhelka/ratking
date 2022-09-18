using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : State {
  protected D_HurtState stateData;

  protected bool isTouchingPlayer;
  protected bool isPlayerInMaxAggroRange;
  protected bool isGrounded;
  protected bool isHurtOver;

  public HurtState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_HurtState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }
  
  public override void Enter() {
    base.Enter();

    isHurtOver = false;
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (!core.Combat.isKnockbackActive) {
      isHurtOver = true;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isTouchingPlayer = entity.CheckTouchingPlayer();
    isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
    isGrounded = core.CollisionSenses.Grounded;
  }
}
