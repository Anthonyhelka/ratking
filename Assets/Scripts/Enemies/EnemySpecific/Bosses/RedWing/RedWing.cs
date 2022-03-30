using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWing : Entity {
  public RedWing_IdleState idleState { get; private set; }
  public RedWing_MoveState moveState { get; private set; }
  public RedWing_PlayerDetectedState playerDetectedState { get; private set; }
  public RedWing_MeleeAttackState meleeAttackState { get; private set; }
  public RedWing_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_MoveState moveStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] public D_MeleeAttackState meleeAttackStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform meleeAttackPosition;

  public override void Start() {
    base.Start();

    idleState = new RedWing_IdleState(this, stateMachine, "idle", idleStateData, this);
    moveState = new RedWing_MoveState(this, stateMachine, "move", moveStateData, this);
    playerDetectedState = new RedWing_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    meleeAttackState = new RedWing_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    deadState = new RedWing_DeadState(this, stateMachine, "dead", deadStateData, this);

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
