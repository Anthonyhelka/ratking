using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedRat : Entity {
  public InfectedRat_IdleState idleState { get; private set; }
  public InfectedRat_MoveState moveState { get; private set; }
  public InfectedRat_HurtState hurtState { get; private set; }
  public InfectedRat_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_MoveState moveStateData;
  [SerializeField] private D_HurtState hurtStateData;
  [SerializeField] private D_DeadState deadStateData;

  public override void Awake() {
    base.Awake();

    idleState = new InfectedRat_IdleState(this, stateMachine, "idle", idleStateData, this);
    moveState = new InfectedRat_MoveState(this, stateMachine, "move", moveStateData, this);
    hurtState = new InfectedRat_HurtState(this, stateMachine, "idle", hurtStateData, this);
    deadState = new InfectedRat_DeadState(this, stateMachine, "dead", deadStateData, this);
  }

  private void Start() {
    stateMachine.Initialize(moveState);
  }

  public override void Damage(AttackDetails attackDetails) {
    base.Damage(attackDetails);

    if (isDead) {
      stateMachine.ChangeState(deadState);
    }
  }

  public override void Knockback(Vector2 angle, float strength, int direction) {
    base.Knockback(angle, strength, direction);
  }
}
