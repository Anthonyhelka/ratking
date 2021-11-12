using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeRat : Entity {
  public StrikeRat_IdleState idleState { get; private set; }
  public StrikeRat_PlayerDetectedState playerDetectedState { get; private set; }
  public StrikeRat_MeleeAttackState meleeAttackState { get; private set; }
  public StrikeRat_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] public D_MeleeAttackState meleeAttackStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform meleeAttackPosition;

  public override void Start() {
    base.Start();

    idleState = new StrikeRat_IdleState(this, stateMachine, "idle", idleStateData, this);
    playerDetectedState = new StrikeRat_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    meleeAttackState = new StrikeRat_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    deadState = new StrikeRat_DeadState(this, stateMachine, "dead", deadStateData, this);

    stateMachine.Initialize(idleState);
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
