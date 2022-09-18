using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongWalker : Entity {
  public LongWalker_IdleState idleState { get; private set; }
  public LongWalker_MoveState moveState { get; private set; }
  public LongWalker_PlayerDetectedState playerDetectedState { get; private set; }
  public LongWalker_ChaseState chaseState { get; private set; }
  public LongWalker_HurtState hurtState { get; private set; }
  public LongWalker_DeadState deadState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_MoveState moveStateData;
  [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
  [SerializeField] private D_ChaseState chaseStateData;
  [SerializeField] private D_HurtState hurtStateData;
  [SerializeField] private D_DeadState deadStateData;

  public override void Awake() {
    base.Awake();

    idleState = new LongWalker_IdleState(this, stateMachine, "idle", idleStateData, this);
    moveState = new LongWalker_MoveState(this, stateMachine, "move", moveStateData, this);
    playerDetectedState = new LongWalker_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    chaseState = new LongWalker_ChaseState(this, stateMachine, "chase", chaseStateData, this);
    hurtState = new LongWalker_HurtState(this, stateMachine, "move", hurtStateData, this);
    deadState = new LongWalker_DeadState(this, stateMachine, "dead", deadStateData, this);
  }

  private void Start() {
    stateMachine.Initialize(moveState);
  }

  public override void Damage(AttackDetails attackDetails) {
    base.Damage(attackDetails);

    if (isDead) {
      stateMachine.ChangeState(deadState);
    }
  }

  public override void Knockback(Vector2 angle, float strength, int direction) {
    base.Knockback(angle, strength, direction);
  }
}