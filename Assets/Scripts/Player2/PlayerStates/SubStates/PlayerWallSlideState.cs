using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState {
  public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) { }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (!isExitingState) {
      player.SetVelocityY(-playerData.wallSlideVelocity);
      if (grabInput && yInput == 0.0f) {
        stateMachine.ChangeState(player.wallGrabState);
      } else if (isGrounded) {
        if (grabInput) {
          stateMachine.ChangeState(player.wallGrabState);
        } else {
          stateMachine.ChangeState(player.idleState);
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
}