using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRat : Entity {
  public KnightRat_IdleState idleState { get; private set; }
  public KnightRat_MoveState moveState { get; private set; }
  public KnightRat_PlayerDetectedState playerDetectedState { get; private set; }
  public KnightRat_ChargeState chargeState { get; private set; }
  public KnightRat_MeleeAttackState meleeAttackState { get; private set; }
  public KnightRat_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_MoveState moveStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] private D_ChargeState chargeStateData;
  [SerializeField] private D_MeleeAttackState meleeAttackStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform meleeAttackPosition;

  public override void Start() {
    base.Start();

    idleState = new KnightRat_IdleState(this, stateMachine, "idle", idleStateData, this);
    moveState = new KnightRat_MoveState(this, stateMachine, "move", moveStateData, this);
    playerDetectedState = new KnightRat_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    chargeState = new KnightRat_ChargeState(this, stateMachine, "charge", chargeStateData, this);
    meleeAttackState = new KnightRat_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    deadState = new KnightRat_DeadState(this, stateMachine, "dead", deadStateData, this);

    stateMachine.Initialize(moveState);
  }

  public override void Damage(AttackDetails attackDetails) {
    base.Damage(attackDetails);

    if (isDead) {
      stateMachine.ChangeState(deadState);
    }
  }

  public override void OnDrawGizmos() {
    base.OnDrawGizmos();

    Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
  }
}
