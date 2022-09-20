using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedTrap : Entity {
  public InfectedTrap_IdleState idleState { get; private set; }
  public InfectedTrap_MeleeAttackState meleeAttackState { get; private set; }
  public InfectedTrap_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_MeleeAttackState meleeAttackStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform meleeAttackPosition;

  public override void Awake() {
    base.Awake();

    idleState = new InfectedTrap_IdleState(this, stateMachine, "idle", idleStateData, this);
    meleeAttackState = new InfectedTrap_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    deadState = new InfectedTrap_DeadState(this, stateMachine, "dead", deadStateData, this);
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
