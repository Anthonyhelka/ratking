using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerRat : Entity {
  public HammerRat_IdleState idleState { get; private set; }
  public HammerRat_PlayerDetectedState playerDetectedState { get; private set; }
  public HammerRat_BlockState blockState { get; private set; }
  public HammerRat_MeleeAttackState meleeAttackState { get; private set; }
  public HammerRat_SpawnUnitState spawnUnitState { get; private set; }
  public HammerRat_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] private D_BlockState blockStateData;
  [SerializeField] public D_MeleeAttackState meleeAttackStateData;
  [SerializeField] public D_SpawnUnitState spawnUnitStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform meleeAttackPosition;
  [SerializeField] private Transform spawnPosition;

  public override void Awake() {
    base.Awake();

    idleState = new HammerRat_IdleState(this, stateMachine, "idle", idleStateData, this);
    playerDetectedState = new HammerRat_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    blockState = new HammerRat_BlockState(this, stateMachine, "block", blockStateData, this);
    meleeAttackState = new HammerRat_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
    spawnUnitState = new HammerRat_SpawnUnitState(this, stateMachine, "spawnUnit", spawnPosition, spawnUnitStateData, this);
    deadState = new HammerRat_DeadState(this, stateMachine, "dead", deadStateData, this);
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