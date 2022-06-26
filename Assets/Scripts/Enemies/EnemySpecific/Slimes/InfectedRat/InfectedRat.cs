using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedRat : Entity {
  public InfectedRat_IdleState idleState { get; private set; }
  public InfectedRat_MoveState moveState { get; private set; }
  public InfectedRat_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_MoveState moveStateData;
  [SerializeField] private D_DeadState deadStateData;

  public override void Start() {
    base.Start();

    idleState = new InfectedRat_IdleState(this, stateMachine, "idle", idleStateData, this);
    moveState = new InfectedRat_MoveState(this, stateMachine, "move", moveStateData, this);
    deadState = new InfectedRat_DeadState(this, stateMachine, "dead", deadStateData, this);

    stateMachine.Initialize(moveState);
  }

  public override void Damage(AttackDetails attackDetails) {
    base.Damage(attackDetails);

    if (isDead) {
      stateMachine.ChangeState(deadState);
    }
  }

  public override void Damage(float amount) {
    base.Damage(amount);

    if (isDead) {
      stateMachine.ChangeState(deadState);
    }
  }
}
