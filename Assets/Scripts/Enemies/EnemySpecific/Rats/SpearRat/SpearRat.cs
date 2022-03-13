using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearRat : Entity {
  public SpearRat_IdleState idleState { get; private set; }
  public SpearRat_PlayerDetectedState playerDetectedState { get; private set; }
  public SpearRat_ChargeState chargeState { get; private set; }
  public SpearRat_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] private D_ChargeState chargeStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform meleeAttackPosition;

  public override void Start() {
    base.Start();

    idleState = new SpearRat_IdleState(this, stateMachine, "idle", idleStateData, this);
    playerDetectedState = new SpearRat_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    chargeState = new SpearRat_ChargeState(this, stateMachine, "charge", chargeStateData, this);
    deadState = new SpearRat_DeadState(this, stateMachine, "dead", deadStateData, this);

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
  }
}
