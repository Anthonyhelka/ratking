using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyRat : Entity {
  public StickyRat_IdleState idleState { get; private set; }
  public StickyRat_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_DeadState deadStateData;

  public override void Start() {
    base.Start();

    idleState = new StickyRat_IdleState(this, stateMachine, "idle", idleStateData, this);
    deadState = new StickyRat_DeadState(this, stateMachine, "dead", deadStateData, this);

    stateMachine.Initialize(idleState);
  }

  public override void FixedUpdate() {
    base.FixedUpdate();

    rb.gravityScale = 0.0f;
  }

  public override void Damage(AttackDetails attackDetails) {
    base.Damage(attackDetails);

    if (isDead) {
      stateMachine.ChangeState(deadState);
    }
  }
}