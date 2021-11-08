using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
  public FiniteStateMachine stateMachine;
  public D_Entity entityData;

  public Rigidbody2D rb { get; private set; }
  public Animator animator { get; private set; }
  public GameObject alive { get; private set; }

  public int facingDirection { get; private set; }
  private Vector2 velocityWorkspace;

  [SerializeField] private Transform wallCheck;
  [SerializeField] private Transform ledgeCheck;
  [SerializeField] private Transform playerCheck;

  public virtual void Start() {
    stateMachine = new FiniteStateMachine();
    alive = transform.Find("Alive").gameObject;
    rb = alive.GetComponent<Rigidbody2D>();
    animator = alive.GetComponent<Animator>();

    facingDirection = 1;
  }

  public virtual void Update() {
    stateMachine.currentState.LogicUpdate();
  }

  public virtual void FixedUpdate() {
    stateMachine.currentState.PhysicsUpdate();
  }

  public virtual void SetVelocity(float velocity) {
    velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
    rb.velocity = velocityWorkspace;
  }

  public virtual bool CheckWall() {
    return Physics2D.Raycast(wallCheck.position, alive.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
  }

  public virtual bool CheckLedge() {
    return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
  }

  public virtual bool CheckPlayerInMinAggroRange() {
    return Physics2D.Raycast(playerCheck.position, alive.transform.right, entityData.minAggroDistance, entityData.whatIsPlayer);
  }

  public virtual bool CheckPlayerInMaxAggroRange() {
    return Physics2D.Raycast(playerCheck.position, alive.transform.right, entityData.maxAggroDistance, entityData.whatIsPlayer);
  }

  public virtual void Flip() {
    facingDirection *= -1;
    alive.transform.Rotate(0.0f, 180.0f, 0.0f);
  }

  public virtual void OnDrawGizmos() {
    Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
    Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
    Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAggroDistance));
    Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.maxAggroDistance));
  }
}
