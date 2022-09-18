using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueFly : Entity {
  public PlagueFly_IdleState idleState { get; private set; }
  public PlagueFly_PlayerDetectedState playerDetectedState { get; private set; }
  public PlagueFly_PathfindState pathfindState { get; private set; }
  public PlagueFly_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] private D_PathfindState pathfindStateData;
  [SerializeField] private D_DeadState deadStateData;

  public override void Awake() {
    base.Awake();

    idleState = new PlagueFly_IdleState(this, stateMachine, "idle", idleStateData, this);
    playerDetectedState = new PlagueFly_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    pathfindState = new PlagueFly_PathfindState(this, stateMachine, "pathfind", pathfindStateData, this);
    deadState = new PlagueFly_DeadState(this, stateMachine, "dead", deadStateData, this);
  }

  private void Start() {
    stateMachine.Initialize(idleState);
  }

  public override void FixedUpdate() {
    base.FixedUpdate();

    Core.Movement.RB.gravityScale = 0.0f;
  }
  
  public override void Damage(AttackDetails attackDetails) {
    base.Damage(attackDetails);

    if (isDead) {
      stateMachine.ChangeState(deadState);
    }
  }
}