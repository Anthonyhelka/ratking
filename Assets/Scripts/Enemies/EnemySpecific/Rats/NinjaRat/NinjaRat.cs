using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaRat : Entity {
  public NinjaRat_IdleState idleState { get; private set; }
  public NinjaRat_MoveState moveState { get; private set; }
  public NinjaRat_PlayerDetectedState playerDetectedState { get; private set; }
  public NinjaRat_DodgeState dodgeState { get; private set; }
  public NinjaRat_RangedAttackState rangedAttackState { get; private set; }
  public NinjaRat_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_MoveState moveStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] public D_DodgeState dodgeStateData;
  [SerializeField] public D_RangedAttackState rangedAttackStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform rangedAttackPosition;

  public override void Start() {
    base.Start();

    idleState = new NinjaRat_IdleState(this, stateMachine, "idle", idleStateData, this);
    moveState = new NinjaRat_MoveState(this, stateMachine, "move", moveStateData, this);
    playerDetectedState = new NinjaRat_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    dodgeState = new NinjaRat_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);
    rangedAttackState = new NinjaRat_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);
    deadState = new NinjaRat_DeadState(this, stateMachine, "dead", deadStateData, this);

    stateMachine.Initialize(moveState);
  }

  public override void Damage(AttackDetails attackDetails) {
    base.Damage(attackDetails);

    if (isDead) {
      stateMachine.ChangeState(deadState);
    } else if (Time.time >= dodgeState.startTime + dodgeStateData.dodgeCooldown) {
      stateMachine.ChangeState(dodgeState);
    }
  }
}
