using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : Entity {
  public Shockwave_MeleeAttackState meleeAttackState { get; private set; }
  public Shockwave_DeadState deadState { get; private set; }

  [SerializeField] private D_MeleeAttackState meleeAttackStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform meleeAttackPosition;

  public override void Awake() {
    base.Awake();

    meleeAttackState = new Shockwave_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    deadState = new Shockwave_DeadState(this, stateMachine, "dead", deadStateData, this);
  }

  private void Start() {
    stateMachine.Initialize(meleeAttackState);
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
