using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyRat : Entity {
  public StickyRat_IdleState idleState { get; private set; }
  public StickyRat_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_DeadState deadStateData;

  public override void Awake() {
    base.Awake();

    idleState = new StickyRat_IdleState(this, stateMachine, "idle", idleStateData, this);
    deadState = new StickyRat_DeadState(this, stateMachine, "dead", deadStateData, this);
  }

  private void Start() {
    stateMachine.Initialize(idleState);
  }

  public override void FixedUpdate() {
    base.FixedUpdate();

    Core.Movement.RB.gravityScale = 0.0f;
  }

  public override void Damage(AttackDetails attackDetails) {
    base.Damage(attackDetails);

    if (isDead) {
      stateMachine.ChangeState(deadState);
    }
  }
}