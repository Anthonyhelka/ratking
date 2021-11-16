using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  #region State Variables
  public PlayerStateMachine stateMachine { get; private set; }
  public PlayerIdleState idleState { get; private set; }
  public PlayerMoveState moveState { get; private set; }
  [SerializeField] private PlayerData playerData;
  #endregion

  #region Components
  public Rigidbody2D rb { get; private set; }
  public Animator animator { get; private set; }
  public PlayerInputHandler inputHandler { get; private set; }
  #endregion

  #region Other Variables
  public int facingDirection { get; private set; }
  public Vector2 currentVelocity { get; private set; }
  private Vector2 velocityWorkspace;
  #endregion

  #region Unity Callback Functions
  private void Awake() {
    stateMachine = new PlayerStateMachine();

    idleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
    moveState = new PlayerMoveState(this, stateMachine, playerData, "move");
  }

  private void Start() {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    inputHandler = GetComponent<PlayerInputHandler>();
    facingDirection = 1;

    stateMachine.Initialize(idleState);
  }

  private void Update() {
    currentVelocity = rb.velocity;

    stateMachine.currentState.LogicUpdate();
  }

  private void FixedUpdate() {
    stateMachine.currentState.PhysicsUpdate();
  }
  #endregion
  
  #region Set Functions
  public void SetVelocityX(float velocity) {
    velocityWorkspace.Set(velocity, currentVelocity.y);
    rb.velocity = velocityWorkspace;
    currentVelocity = velocityWorkspace;
  }
  #endregion

  #region Check Functions
    public void CheckIfShouldFlip(int xInput) {
      if (xInput != 0 && xInput != facingDirection) {
        Flip();
      } 
    }
  #endregion
  
  #region Other Functions
  private void Flip() {
    facingDirection *= -1;
    transform.Rotate(0.0f, 180.0f, 0.0f);
  }
  #endregion
}
