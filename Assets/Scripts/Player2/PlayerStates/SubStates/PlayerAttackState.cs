using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState {
  private Weapon weapon;

  public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName) {
  }

  public override void Enter() {
    base.Enter();

    weapon.EnterWeapon();
  }

  public override void Exit() {
    base.Exit();

    weapon.ExitWeapon();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
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
}