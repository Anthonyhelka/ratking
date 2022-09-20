using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRat : Entity {
  public BombRat_IdleState idleState { get; private set; }
  public BombRat_MoveState moveState { get; private set; }
  public BombRat_PlayerDetectedState playerDetectedState { get; private set; }
  public BombRat_FleeState fleeState { get; private set; }
  public BombRat_SpawnUnitState spawnUnitState { get; private set; }
  public BombRat_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_MoveState moveStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] private D_FleeState fleeStateData;
  public D_SpawnUnitState spawnUnitStateData;
  [SerializeField] private D_DeadState deadStateData;

  [SerializeField] private Transform spawnPosition;

  public override void Awake() {
    base.Awake();

    idleState = new BombRat_IdleState(this, stateMachine, "idle", idleStateData, this);
    moveState = new BombRat_MoveState(this, stateMachine, "move", moveStateData, this);
    playerDetectedState = new BombRat_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    fleeState = new BombRat_FleeState(this, stateMachine, "flee", fleeStateData, this);
    spawnUnitState = new BombRat_SpawnUnitState(this, stateMachine, "spawnUnit", spawnPosition, spawnUnitStateData, this);
    deadState = new BombRat_DeadState(this, stateMachine, "dead", deadStateData, this);
  }

  private void Start() {
    stateMachine.Initialize(moveState);
  }

  public override void Damage(AttackDetails attackDetails) {
    base.Damage(attackDetails);

    if (isDead) {
      GameObject bomb = GameObject.Instantiate(spawnUnitStateData.unit, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.1f,gameObject.transform.position.z), gameObject.transform.rotation);
      bomb.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
      stateMachine.ChangeState(deadState);
    }
  }
}
