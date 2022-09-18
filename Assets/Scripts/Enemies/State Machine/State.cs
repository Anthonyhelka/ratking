using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {
  protected Entity entity;
  protected FiniteStateMachine stateMachine;
  protected Core core;

  public float startTime { get; protected set; }

  protected string animationBoolName;

  public State(Entity entity, FiniteStateMachine stateMachine, string animationBoolName) {
    this.entity = entity;
    this.stateMachine = stateMachine;
    this.animationBoolName = animationBoolName;
    core = entity.Core;
  }

  public virtual void Enter() {
    startTime = Time.time;
    entity.animator.SetBool(animationBoolName, true);

    DoChecks();
  }

  public virtual void Exit() {
    entity.animator.SetBool(animationBoolName, false);
  }

  public virtual void LogicUpdate() { }

  public virtual void PhysicsUpdate() {
    DoChecks();
  }

  public virtual void DoChecks() { }
}
