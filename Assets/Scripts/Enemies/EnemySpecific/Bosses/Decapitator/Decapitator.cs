using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decapitator : Entity {
  public Decapitator_IdleState idleState { get; private set; }
  public Decapitator_PlayerDetectedState playerDetectedState { get; private set; }
  public Decapitator_ChargeState chargeState { get; private set; }
  public Decapitator_CooldownState cooldownState { get; private set; }
  public Decapitator_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] private D_ChargeState chargeStateData;
  [SerializeField] private D_CooldownState cooldownStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform meleeAttackPosition;

  public override void Awake() {
    base.Awake();

    idleState = new Decapitator_IdleState(this, stateMachine, "idle", idleStateData, this);
    playerDetectedState = new Decapitator_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    chargeState = new Decapitator_ChargeState(this, stateMachine, "charge", chargeStateData, this);
    cooldownState = new Decapitator_CooldownState(this, stateMachine, "cooldown", cooldownStateData, this);
    deadState = new Decapitator_DeadState(this, stateMachine, "dead", deadStateData, this);
  }

  private void Start() {
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

    // Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
  }
}
