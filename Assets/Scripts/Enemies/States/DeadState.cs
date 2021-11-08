using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State {
  protected D_DeadState stateData;

  public DeadState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeadState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }
  
  public override void Enter() {
    base.Enter();

    GameObject.Instantiate(stateData.deathBloodParticle, entity.alive.transform.position, stateData.deathBloodParticle.transform.rotation);

    entity.gameObject.SetActive(false);
  }

  public override void Exit() {
    base.Exit();
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
}
