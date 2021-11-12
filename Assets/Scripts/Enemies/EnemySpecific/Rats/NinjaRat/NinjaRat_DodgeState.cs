using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaRat_DodgeState : DodgeState {
  private NinjaRat ninjaRat;

  public NinjaRat_DodgeState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DodgeState stateData, NinjaRat ninjaRat) : base(entity, stateMachine, animationBoolName, stateData) {
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

    if (isDodgeOver) {
      if (isPlayerInMaxAggroRange) {
        if (Time.time > ninjaRat.rangedAttackState.startTime + ninjaRat.rangedAttackStateData.attackCooldown) {
          stateMachine.ChangeState(ninjaRat.rangedAttackState);
        } else {
          stateMachine.ChangeState(ninjaRat.playerDetectedState);
        }
      } else if (!isPlayerInMaxAggroRange) {
        stateMachine.ChangeState(ninjaRat.moveState);
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