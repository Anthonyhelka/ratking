using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Entity {
  public Bomb_CooldownState cooldownState { get; private set; }
  public Bomb_MeleeAttackState meleeAttackState { get; private set; }
  public Bomb_DeadState deadState { get; private set; }

  [SerializeField] private D_CooldownState cooldownStateData;
  [SerializeField] private D_MeleeAttackState meleeAttackStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform meleeAttackPosition;

  public override void Awake() {
    base.Awake();

    cooldownState = new Bomb_CooldownState(this, stateMachine, "cooldown", cooldownStateData, this);
    meleeAttackState = new Bomb_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    deadState = new Bomb_DeadState(this, stateMachine, "dead", deadStateData, this);
  }

  private void Start() {
    stateMachine.Initialize(cooldownState);
  }

  public override void Damage(AttackDetails attackDetails) {
    base.Damage(attackDetails);

    if (isDead) {
      stateMachine.ChangeState(deadState);
    }
  }
}
