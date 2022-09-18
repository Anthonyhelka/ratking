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

    entity.seeker.StartPath(core.Movement.RB.position, entity.lastPlayerDetectedPosition, OnPathComplete);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
    
    if (core.Movement.FacingDirection != (entity.lastPlayerDetectedPosition.x <= entity.transform.position.x ? -1 : 1)) {
      core.Movement.Flip();
    }

    if (currentWaypoint < path.vectorPath.Count) {
      if (Vector2.Distance(core.Movement.RB.position, path.vectorPath[currentWaypoint]) < stateData.nextWaypointDistance) {
        currentWaypoint++;
      }
      core.Movement.SetVelocity(stateData.pathfindSpeed, ((Vector2)path.vectorPath[currentWaypoint] - core.Movement.RB.position).normalized);
      entity.seeker.StartPath(core.Movement.RB.position, entity.lastPlayerDetectedPosition, OnPathComplete); 
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
