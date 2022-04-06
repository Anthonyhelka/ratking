using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState {
  protected Player player;
  protected PlayerStateMachine stateMachine;
  protected PlayerData playerData;
  protected float startTime;
  private string animationBoolName;
  protected bool isAnimationFinished;

  public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) {
    this.player = player;
    this.stateMachine = stateMachine;
    this.playerData = playerData;
    this.animationBoolName = animationBoolName;
  }

  public virtual void Enter() {
    startTime = Time.time;
    player.Anim.SetBool(animationBoolName, true);
    isAnimationFinished = false;
    DoChecks();
    // Debug.Log(animationBoolName);
  }

  public virtual void Exit() {
    player.Anim.SetBool(animationBoolName, false);
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
