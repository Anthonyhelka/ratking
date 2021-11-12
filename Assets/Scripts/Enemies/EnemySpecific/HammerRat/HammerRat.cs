using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerRat : Entity {
  public HammerRat_IdleState idleState { get; private set; }
  public HammerRat_PlayerDetectedState playerDetectedState { get; private set; }
  public HammerRat_BlockState blockState { get; private set; }
  public HammerRat_MeleeAttackState meleeAttackState { get; private set; }
  public HammerRat_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] private D_BlockState blockStateData;
  [SerializeField] public D_MeleeAttackState meleeAttackStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform meleeAttackPosition;

  public override void Start() {
    base.Start();

    idleState = new HammerRat_IdleState(this, stateMachine, "idle", idleStateData, this);
    playerDetectedState = new HammerRat_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    blockState = new HammerRat_BlockState(this, stateMachine, "block", blockStateData, this);
    meleeAttackState = new HammerRat_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    deadState = new HammerRat_DeadState(this, stateMachine, "dead", deadStateData, this);

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