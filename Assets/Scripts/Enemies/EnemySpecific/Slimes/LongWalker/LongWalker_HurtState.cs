using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongWalker_HurtState : HurtState {
  private LongWalker longWalker;

  public LongWalker_HurtState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_HurtState stateData, LongWalker longWalker) : base(entity, stateMachine, animationBoolName, stateData) {
    this.longWalker = longWalker;
  }

  public override void Enter() {
    base.Enter();
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (isHurtOver) {
      stateMachine.ChangeState(longWalker.moveState);
    }

    // if (isTouchingPlayer) {
    //   AttackDetails attackDetails;
    //   attackDetails.position = entity.transform.position;
    //   attackDetails.damageAmount = entity.entityData.touchDamageAmount;
    //   attackDetails.type = entity.entityData.type;
    //   entity.lastPlayerTouched.transform.SendMessage("Damage", attackDetails);
    // }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
  }
}
