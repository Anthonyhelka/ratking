using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRat : Entity {
  public KnightRat_IdleState idleState { get; private set; }
  public KnightRat_MoveState moveState { get; private set; }

  [SerializeField] private D_IdleState idleStateData;
  [SerializeField] private D_MoveState moveStateData;

  public override void Start() {
    base.Start();

    idleState = new KnightRat_IdleState(this, stateMachine, "idle", idleStateData, this);
    moveState = new KnightRat_MoveState(this, stateMachine, "move", moveStateData, this);

    stateMachine.Initialize(moveState);
  }
}
