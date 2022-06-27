using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerAbilityState {
  public PlayerDeadState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    switch(player.lastHitAttackDetails.type) {
      case "Infected":
        player.Anim.SetInteger("deathType", 0);
        break;
      case "Spikes":
        player.Anim.SetInteger("deathType", 1);
        break;
      case "Knight":
        player.Anim.SetInteger("deathType", 2);
        break;
      case "Strike":
        player.Anim.SetInteger("deathType", 3);
        break;
      default:
        player.Anim.SetInteger("deathType", 0);
        break;
    }
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    core.Movement.SetVelocityX(0.0f);
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public override void AnimationFinishTrigger() {
    base.AnimationFinishTrigger();

    player.GameOverMenu.GameOver();
  }
}