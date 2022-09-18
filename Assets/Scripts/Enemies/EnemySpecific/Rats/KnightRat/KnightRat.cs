using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRat : Entity {
  public KnightRat_IdleState idleState { get; private set; }
  public KnightRat_MoveState moveState { get; private set; }
  public KnightRat_PlayerDetectedState playerDetectedState { get; private set; }
  public KnightRat_ChargeState chargeState { get; private set; }
  public KnightRat_MeleeAttackState meleeAttackState { get; private set; }
  public KnightRat_StunState stunState { get; private set; }
  public KnightRat_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_MoveState moveStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] private D_ChargeState chargeStateData;
  [SerializeField] public D_MeleeAttackState meleeAttackStateData;
  [SerializeField] private D_StunState stunStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform meleeAttackPosition;

  public override void Awake() {
    base.Awake();

    idleState = new KnightRat_IdleState(this, stateMachine, "idle", idleStateData, this);
    moveState = new KnightRat_MoveState(this, stateMachine, "move", moveStateData, this);
    playerDetectedState = new KnightRat_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    chargeState = new KnightRat_ChargeState(this, stateMachine, "charge", chargeStateData, this);
    meleeAttackState = new KnightRat_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    stunState = new KnightRat_StunState(this, stateMachine, "stun", stunStateData, this);
    deadState = new KnightRat_DeadState(this, stateMachine, "dead", deadStateData, this);
  }

  private void Start() {
    stateMachine.Initialize(moveState);
  }

  public override void Damage(AttackDetails attackDetails) {
    base.Damage(attackDetails);

    if (isDead) {
      stateMachine.ChangeState(deadState);
    } else if (lastPlayerDetectedPosition.y > transform.position.y + 0.275f && Time.time > stunState.nextStunTime) {
      stateMachine.ChangeState(stunState);
    }
  }

  public override void OnDrawGizmos() {
    base.OnDrawGizmos();

    Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
  }
}
