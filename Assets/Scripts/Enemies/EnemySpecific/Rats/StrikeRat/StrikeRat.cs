using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeRat : Entity {
  public StrikeRat_IdleState idleState { get; private set; }
  public StrikeRat_PlayerDetectedState playerDetectedState { get; private set; }
  public StrikeRat_TeleportState teleportState { get; private set; }
  public StrikeRat_MeleeAttackState meleeAttackState { get; private set; }
  public StrikeRat_CooldownState cooldownState { get; private set; }
  public StrikeRat_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] private D_TeleportState teleportStateData;
  [SerializeField] public D_MeleeAttackState meleeAttackStateData;
  [SerializeField] public D_CooldownState cooldownStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform meleeAttackPosition;

  public override void Awake() {
    base.Awake();

    idleState = new StrikeRat_IdleState(this, stateMachine, "idle", idleStateData, this);
    playerDetectedState = new StrikeRat_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    teleportState = new StrikeRat_TeleportState(this, stateMachine, "teleport", teleportStateData, this);
    meleeAttackState = new StrikeRat_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    cooldownState = new StrikeRat_CooldownState(this, stateMachine, "cooldown", cooldownStateData, this);
    deadState = new StrikeRat_DeadState(this, stateMachine, "dead", deadStateData, this);
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

    Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
  }
}
