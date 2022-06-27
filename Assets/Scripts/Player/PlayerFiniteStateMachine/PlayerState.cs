using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState {
  protected Player player;
  protected PlayerStateMachine stateMachine;
  protected PlayerData playerData;
  protected Core core;
  protected float startTime;
  private string animationBoolName;
  protected bool isAnimationFinished;
  protected bool isExitingState;

  public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) {
    this.player = player;
    this.stateMachine = stateMachine;
    this.playerData = playerData;
    core = player.core;
    this.animationBoolName = animationBoolName;
  }

  public virtual void Enter() {
    startTime = Time.time;
    player.Anim.SetBool(animationBoolName, true);
    isAnimationFinished = false;
    isExitingState = false;
    DoChecks();

    // Debug.Log(animationBoolName);
  }

  public virtual void Exit() {
    player.Anim.SetBool(animationBoolName, false);

    isExitingState = true;
  }

  public virtual void LogicUpdate() { }

  public virtual void PhysicsUpdate() {
    DoChecks();
  }

  public virtual void DoChecks() { }

  public virtual void AnimationTrigger() { }

  public virtual void AnimationFinishTrigger() {
    isAnimationFinished = true;
  }

  public virtual void DrawGizmos() {
    core.DrawGizmos();
  }
}
