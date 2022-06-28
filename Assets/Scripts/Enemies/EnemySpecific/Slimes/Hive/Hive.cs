using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : Entity {
  public Hive_IdleState idleState { get; private set; }
  public Hive_PlayerDetectedState playerDetectedState { get; private set; }
  public Hive_SpawnUnitState spawnUnitState { get; private set; }
  public Hive_CooldownState cooldownState { get; private set; }
  public Hive_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] private D_SpawnUnitState spawnUnitStateData;
  [SerializeField] private D_CooldownState cooldownStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform spawnPosition;

  public override void Start() {
    base.Start();

    idleState = new Hive_IdleState(this, stateMachine, "idle", idleStateData, this);
    playerDetectedState = new Hive_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    spawnUnitState = new Hive_SpawnUnitState(this, stateMachine, "spawnUnit", spawnPosition, spawnUnitStateData, this);
    cooldownState = new Hive_CooldownState(this, stateMachine, "cooldown", cooldownStateData, this);
    deadState = new Hive_DeadState(this, stateMachine, "dead", deadStateData, this);

    stateMachine.Initialize(idleState);
  }

  public override void FixedUpdate() {
    base.FixedUpdate();

    rb.gravityScale = 0.0f;
  }

  public override void Damage(AttackDetails attackDetails) {
    base.Damage(attackDetails);

    if (isDead) {
      stateMachine.ChangeState(deadState);
    }
  }
}
