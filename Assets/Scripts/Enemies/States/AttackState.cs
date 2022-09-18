using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State {
  protected Transform attackPosition;
  protected bool isAnimationFinished;
  protected bool isPlayerInMinAggroRange;

  public AttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition) : base(entity, stateMachine, animationBoolName) {
    this.attackPosition = attackPosition;
  }
  
  public override void Enter() {
    base.Enter();

    entity.atsm.attackState = this;
    isAnimationFinished = false;
    core.Movement.SetVelocityZero();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.SetVelocityZero();
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
  }

  public virtual void TriggerAttack() { }
  
  public virtual void FinishAttack() {    
    isAnimationFinished = true;
  }
}
