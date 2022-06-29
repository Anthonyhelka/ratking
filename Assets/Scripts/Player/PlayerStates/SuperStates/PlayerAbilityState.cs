using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState {
  // Inputs
  protected int xInput;
  protected int yInput;
  protected bool jumpInput;
  protected bool dashInput;
  protected bool specialInput;
  protected bool primaryAttackInput;
  protected bool secondaryAttackInput;

  protected bool isAbilityDone;
  protected bool isGrounded;
  protected bool isHurt;
  protected bool isDead;

  public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    isAbilityDone = false;
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    xInput = player.InputHandler.NormalizedInputX;
    yInput = player.InputHandler.NormalizedInputY;
    jumpInput = player.InputHandler.JumpInput;
    dashInput = player.InputHandler.DashInput;
    specialInput = player.InputHandler.SpecialInput;
    primaryAttackInput = player.InputHandler.PrimaryAttackInput;
    secondaryAttackInput = player.InputHandler.SecondaryAttackInput;

    if (isHurt) {
      player.isHurt = false;
      stateMachine.ChangeState(player.HurtState);
    } else if (isAbilityDone) {
      if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f) {
        stateMachine.ChangeState(player.IdleState);
      } else {
        stateMachine.ChangeState(player.InAirState);
      }
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isGrounded = core.CollisionSenses.Grounded;
    isHurt = player.isHurt;
  }

  public override void DrawGizmos() {
    base.DrawGizmos();
  }
}
