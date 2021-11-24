using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState {
  protected Vector2 detectedPosition;
  protected Vector2 cornerPosition;
  protected Vector2 startPosition;
  protected Vector2 stopPosition;
  protected bool isHanging;
  protected bool isClimbing;
  protected int xInput;
  protected int yInput;
  protected bool jumpInput;

  public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) { }

  public override void Enter() {
    base.Enter();

    player.SetVelocityZero();
    player.transform.position = detectedPosition;
    cornerPosition = player.DetermineCornerPosition();

    startPosition.Set(cornerPosition.x - (playerData.startOffset.x * player.facingDirection), cornerPosition.y - playerData.startOffset.y);
    stopPosition.Set(cornerPosition.x + (playerData.stopOffset.x * player.facingDirection), cornerPosition.y + playerData.stopOffset.y);

    player.transform.position = startPosition;
  }

  public override void Exit() {
    base.Exit();

    isHanging = false;

    if (isClimbing) {
      player.transform.position = stopPosition;
      isClimbing = false;
    }
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (!isExitingState) {
      if (isAnimationFinished) {
        stateMachine.ChangeState(player.idleState);
      } else {
        xInput = player.inputHandler.normalizedInputX;
        yInput = player.inputHandler.normalizedInputY;
        jumpInput = player.inputHandler.jumpInput;

        player.SetVelocityZero();
        player.transform.position = startPosition;

        if ((xInput == player.facingDirection || yInput == 1) && isHanging && !isClimbing) {
          isClimbing = true;
          player.animator.SetBool("ledgeClimb", true);
        } else if ((xInput == -player.facingDirection || yInput == -1) && isHanging && !isClimbing) {
          stateMachine.ChangeState(player.inAirState);
        } else if (jumpInput && !isClimbing) {
          player.wallJumpState.DetermineWallJumpDirection(true);
          stateMachine.ChangeState(player.wallJumpState);
        }
      }
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public void SetDetectedPosition(Vector2 position) {
    detectedPosition = position;
  }

  public override void AnimationTrigger() {
    base.AnimationTrigger();

    isHanging = true;
  }

  public override void AnimationFinishTrigger() {
    base.AnimationFinishTrigger();

    player.animator.SetBool("ledgeClimb", false);
  }
}
