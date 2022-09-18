using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyShooter : Entity {
  public StickyShooter_IdleState idleState { get; private set; }
  public StickyShooter_PlayerDetectedState playerDetectedState { get; private set; }
  public StickyShooter_RangedAttackState rangedAttackState { get; private set; }
  public StickyShooter_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] public D_RangedAttackState rangedAttackStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform rangedAttackPosition;

  public override void Awake() {
    base.Awake();

    idleState = new StickyShooter_IdleState(this, stateMachine, "idle", idleStateData, this);
    playerDetectedState = new StickyShooter_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    rangedAttackState = new StickyShooter_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);
    deadState = new StickyShooter_DeadState(this, stateMachine, "dead", deadStateData, this);
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