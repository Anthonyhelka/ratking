using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : State {
  protected D_BlockState stateData;

  protected bool isMinBlockTimeOver;
  protected bool isPlayerInMinAggroRange;
  protected bool isPlayerInMaxAggroRange;
  protected bool performCloseRangeAction;
  protected bool isTouchingPlayer;

  public BlockState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_BlockState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }
  
  public override void Enter() {
    base.Enter();

    isMinBlockTimeOver = false;
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (Time.time >= startTime + stateData.minBlockTime) {
      isMinBlockTimeOver = true;
    }

    // if (entityData.touchDamage && isTouchingPlayer) {
    //   IDamageable damageable = entity.lastPlayerTouched.GetComponent<IDamageable>();
    //   if (damageable != null) {
    //     damageable.Damage(attackDetails);
    //   }
    // }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
    performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    isTouchingPlayer = entity.CheckTouchingPlayer();
  }
}
