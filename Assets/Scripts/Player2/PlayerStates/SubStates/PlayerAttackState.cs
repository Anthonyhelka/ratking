using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState {
  private Weapon weapon;
  private float velocityToSet;
  private bool setVelocity;
  private bool shouldCheckFlip;

  public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    setVelocity = false;

    weapon.EnterWeapon();
  }

  public override void Exit() {
    base.Exit();

    weapon.ExitWeapon();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (shouldCheckFlip) {
      player.CheckIfShouldFlip(xInput);
    }

    if (setVelocity) {
      player.SetVelocityX(velocityToSet * player.FacingDirection);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }

  public override void AnimationTrigger() {
    base.AnimationTrigger();
  }

  public override void AnimationFinishTrigger() {
    base.AnimationFinishTrigger();

    isAbilityDone = true;
  }

  public void SetWeapon(Weapon weapon) {
    this.weapon = weapon;
    this.weapon.InitializeWeapon(this);
  }

  public void SetPlayerVelocity(float velocity) {
    player.SetVelocityX(velocity * player.FacingDirection);

    velocityToSet = velocity;
    setVelocity = true;
  }

  public void SetFlipCheck(bool value) {
    shouldCheckFlip = value;
  }
}