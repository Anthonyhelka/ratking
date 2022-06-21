using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  #region State Variables
  public PlayerStateMachine StateMachine { get; private set; }
  public PlayerIdleState IdleState { get; private set; }
  public PlayerMoveState MoveState { get; private set; }
  public PlayerJumpState JumpState { get; private set; }
  public PlayerDoubleJumpState DoubleJumpState { get; private set; }
  public PlayerInAirState InAirState { get; private set; }
  public PlayerLandState LandState { get; private set; }
  [SerializeField] private PlayerData playerData;
  // Specials
  // Boomerang
  public PlayerBoomerangThrowState BoomerangThrowState { get; private set; }
  public PlayerBoomerangCatchState BoomerangCatchState { get; private set; }
  // Shield
  // JetPack
  // Glider
  public PlayerGlideState GlideState { get; private set; }
  #endregion
  
  #region Components
  public Animator Anim { get; private set; }
  public Rigidbody2D RB { get; private set; }
  public PlayerInputHandler InputHandler { get; private set; }
  #endregion
  
  #region Check Transforms
  [SerializeField] private Transform groundCheck;
  public Transform boomerangPosition;
  #endregion

  #region Other Variables
  private Vector2 workspace;
  public Vector2 CurrentVelocity { get; private set; }
  public int FacingDirection { get; private set; }
  #endregion

  #region Unity Functions
  private void Awake() {
    StateMachine = new PlayerStateMachine();
    IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
    MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
    JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
    DoubleJumpState = new PlayerDoubleJumpState(this, StateMachine, playerData, "inAir");
    InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
    LandState = new PlayerLandState(this, StateMachine, playerData, "land");
    // Specials
    // Boomerang
    BoomerangThrowState = new PlayerBoomerangThrowState(this, StateMachine, playerData, "boomerangThrow");
    BoomerangCatchState = new PlayerBoomerangCatchState(this, StateMachine, playerData, "boomerangCatch");
    // Shield
    // JetPack
    // Glider
    GlideState = new PlayerGlideState(this, StateMachine, playerData, "glide");
  }

  private void Start() {
    Anim = GetComponent<Animator>();
    RB = GetComponent<Rigidbody2D>();
    InputHandler = GetComponent<PlayerInputHandler>();

    FacingDirection = 1;
    StateMachine.Initialize(IdleState);
  }

  private void Update() {
    CurrentVelocity = RB.velocity;
    StateMachine.CurrentState.LogicUpdate();
  }

  private void FixedUpdate() {
    StateMachine.CurrentState.PhysicsUpdate();
  }
  #endregion

  #region Set Functions
  public void SetVelocityX(float velocity) {
    workspace.Set(velocity, CurrentVelocity.y);
    RB.velocity = workspace;
    CurrentVelocity = workspace;
  }

  public void SetVelocityY(float velocity) {
    workspace.Set(CurrentVelocity.x, velocity);
    RB.velocity = workspace;
    CurrentVelocity = workspace;
  }
  #endregion

  #region Check Functions
  public bool CheckIfGrounded() {
    return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
  }

  public void CheckIfShouldFlip(int xInput) {
    if (xInput != 0 && xInput != FacingDirection) {
      Flip();
    }
  }
  #endregion

  #region Other Functions
  private void AnimationTrigger() {
    StateMachine.CurrentState.AnimationTrigger();
  }

  private void AnimationFinishTrigger() {
    StateMachine.CurrentState.AnimationFinishTrigger();
  }

  private void Flip() {
    FacingDirection *= -1;
    transform.Rotate(0.0f, 180.0f, 0.0f);
  }
  #endregion

  #region Gizmos
  public virtual void OnDrawGizmos() {
    // Ground Check
    Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckRadius);
  }
  #endregion
}
