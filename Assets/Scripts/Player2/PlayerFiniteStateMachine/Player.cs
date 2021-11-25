using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  #region State Variables
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerLandState landState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallGrabState wallGrabState { get; private set; }
    public PlayerWallClimbState wallClimbState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerLedgeClimbState ledgeClimbState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    [SerializeField] private PlayerData playerData;
  #endregion

  #region Components
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public PlayerInputHandler inputHandler { get; private set; }
    public Transform dashDirectionIndicator { get; private set; }
  #endregion

  #region Check Transforms
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
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
      jumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
      inAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");
      landState = new PlayerLandState(this, stateMachine, playerData, "land");
      wallSlideState = new PlayerWallSlideState(this, stateMachine, playerData, "wallSlide");
      wallGrabState = new PlayerWallGrabState(this, stateMachine, playerData, "wallGrab");
      wallClimbState = new PlayerWallClimbState(this, stateMachine, playerData, "wallClimb");
      wallJumpState = new PlayerWallJumpState(this, stateMachine, playerData, "inAir");
      ledgeClimbState = new PlayerLedgeClimbState(this, stateMachine, playerData, "ledgeClimbState");
      dashState = new PlayerDashState(this, stateMachine, playerData, "inAir");
    }

    private void Start() {
      rb = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
      inputHandler = GetComponent<PlayerInputHandler>();
      dashDirectionIndicator = transform.Find("DashDirectionIndicator");
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
    public void SetVelocityZero() {
      velocityWorkspace.Set(0.0f, 0.0f);
      rb.velocity = velocityWorkspace;
      currentVelocity = velocityWorkspace;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction) {
      angle.Normalize();
      velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
      rb.velocity = velocityWorkspace;
      currentVelocity = velocityWorkspace;
    }

    public void SetVelocity(float velocity, Vector2 direction) {
      velocityWorkspace = direction * velocity;
      rb.velocity = velocityWorkspace;
      currentVelocity = velocityWorkspace;
    }

    public void SetVelocityX(float velocity) {
      velocityWorkspace.Set(velocity, currentVelocity.y);
      rb.velocity = velocityWorkspace;
      currentVelocity = velocityWorkspace;
    }

    public void SetVelocityY(float velocity) {
      velocityWorkspace.Set(currentVelocity.x, velocity);
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

    public bool CheckIfGrounded() {
      return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWall() {
      return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWallBack() {
      return Physics2D.Raycast(wallCheck.position, Vector2.right * -facingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingLedge() {
      return Physics2D.Raycast(ledgeCheck.position, Vector2.right * facingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }
  #endregion
  
  #region Other Functions
    private void Flip() {
      facingDirection *= -1;
      transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void AnimationTrigger() {
      stateMachine.currentState.AnimationTrigger();
    }

    private void AnimationFinishTrigger() {
      stateMachine.currentState.AnimationFinishTrigger();
    }

    public Vector2 DetermineCornerPosition() {
      RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
      float xDistance = xHit.distance;
      velocityWorkspace.Set((xDistance + 0.015f) * facingDirection, 0.0f);
      RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(velocityWorkspace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f, playerData.whatIsGround);
      float yDistance = yHit.distance;
      velocityWorkspace.Set(wallCheck.position.x + (xDistance * facingDirection), ledgeCheck.position.y - yDistance);
      return velocityWorkspace;
    }
  #endregion
}
