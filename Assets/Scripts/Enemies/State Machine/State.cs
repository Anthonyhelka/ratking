using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {
  protected Entity entity;
  protected FiniteStateMachine stateMachine;

  protected float startTime;

  protected string animationBoolName;

  public State(Entity entity, FiniteStateMachine stateMachine, string animationBoolName) {
    this.entity = entity;
    this.stateMachine = stateMachine;
    this.animationBoolName = animationBoolName;
  }

  public virtual void Enter() {
    startTime = Time.time;
    entity.animator.SetBool(animationBoolName, true);
  }

  public virtual void Exit() {
    entity.animator.SetBool(animationBoolName, false);
  }

  public virtual void LogicUpdate() { }

  public virtual void PhysicsUpdate() { }
}
