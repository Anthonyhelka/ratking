using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathfindState : State {
  protected D_PathfindState stateData;

  protected bool isPlayerInMinAggroRange;
  protected bool isPlayerInMaxAggroRange;
  protected bool performCloseRangeAction;
  protected bool isTouchingPlayer;

  protected Path path;
  protected int currentWaypoint = 0;

  public PathfindState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PathfindState stateData) : base(entity, stateMachine, animationBoolName) {
    this.stateData = stateData;
  }
  
  public override void Enter() {
    base.Enter();

    entity.seeker.StartPath(entity.rb.position, entity.lastPlayerDetectedPosition, OnPathComplete);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();

    if (entity.facingDirection != (entity.lastPlayerDetectedPosition.x <= entity.alive.transform.position.x ? -1 : 1)) {
      entity.Flip();
    }

    if (currentWaypoint < path.vectorPath.Count) {
      if (Vector2.Distance(entity.rb.position, path.vectorPath[currentWaypoint]) < stateData.nextWaypointDistance) {
        currentWaypoint++;
      }
      entity.SetVelocity(stateData.pathfindSpeed, ((Vector2)path.vectorPath[currentWaypoint] - entity.rb.position).normalized);
      entity.seeker.StartPath(entity.rb.position, entity.lastPlayerDetectedPosition, OnPathComplete); 
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();

    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
    performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    isTouchingPlayer = entity.CheckTouchingPlayer();
  }

  private void OnPathComplete(Path p) {
    if (!p.error) {
      path = p;
      currentWaypoint = 0;
    }
  }
}
