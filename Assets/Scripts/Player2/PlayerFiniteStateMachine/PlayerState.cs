using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState {
  protected Player player;
  protected PlayerStateMachine stateMachine;
  protected PlayerData playerData;
  protected float startTime;
  protected string animationBoolName;
  protected bool isAnimationFinished;
  protected bool isExitingState;

  public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) {
    this.player = player;
    this.stateMachine = stateMachine;
    this.playerData = playerData;
    this.animationBoolName = animationBoolName;
  }

  public virtual void Enter() {
    DoChecks();
    startTime = Time.time;
    player.animator.SetBool(animationBoolName, true);
    isAnimationFinished = false;
    isExitingState = false;
  }

  public virtual void Exit() {
    player.animator.SetBool(animationBoolName, false);
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
}
