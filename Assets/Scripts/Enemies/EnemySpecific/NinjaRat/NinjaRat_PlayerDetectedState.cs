using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaRat_PlayerDetectedState : PlayerDetectedState {
  private NinjaRat ninjaRat;

  public NinjaRat_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PlayerDetectedState stateData, NinjaRat ninjaRat) : base(entity, stateMachine, animationBoolName, stateData) {
    this.ninjaRat = ninjaRat;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    entity.SetVelocity(0.0f);

    if (performCloseRangeAction) {
      if (Time.time >= ninjaRat.dodgeState.startTime + ninjaRat.dodgeStateData.dodgeCooldown) {
        stateMachine.ChangeState(ninjaRat.dodgeState);
      }
    } else if (Time.time > ninjaRat.rangedAttackState.startTime + ninjaRat.rangedAttackStateData.attackCooldown) {
      stateMachine.ChangeState(ninjaRat.rangedAttackState);
    } else if (!isPlayerInMaxAggroRange) {
      stateMachine.ChangeState(ninjaRat.moveState);
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
