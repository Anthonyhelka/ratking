using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  #region State Variables
  public PlayerStateMachine StateMachine { get; private set; }
  // Movement
  public PlayerIdleState IdleState { get; private set; }
  public PlayerMoveState MoveState { get; private set; }
  public PlayerJumpState JumpState { get; private set; }
  public PlayerDoubleJumpState DoubleJumpState { get; private set; }
  public PlayerDashState DashState { get; private set; }
  public PlayerInAirState InAirState { get; private set; }
  public PlayerLandState LandState { get; private set; }
  public PlayerBounceState BounceState { get; private set; }
  public PlayerRollState RollState { get; private set; }
  public PlayerSleepState SleepState { get; private set; }
  // Combat
  public PlayerPrimaryGroundAttackState PrimaryGroundAttackState { get; private set; }
  public PlayerPrimaryAirAttackState PrimaryAirAttackState { get; private set; }
  public PlayerSecondaryGroundAttackState SecondaryGroundAttackState { get; private set; }
  public PlayerSecondaryAirAttackState SecondaryAirAttackState { get; private set; }
  [SerializeField] private PlayerData playerData;
  // Specials
  // Boomerang
  public PlayerBoomerangThrowState BoomerangThrowState { get; private set; }
  public PlayerBoomerangCatchState BoomerangCatchState { get; private set; }
  // Shield
  public PlayerStartBlockState StartBlockState { get; private set; }
  public PlayerBlockState BlockState { get; private set; }
  public PlayerEndBlockState EndBlockState { get; private set; }
  // JetPack
  // Glider
  public PlayerGlideState GlideState { get; private set; }

  // Checks
  public Transform attackCheck;
  public LayerMask whatIsDamageable;
  #endregion
  
  #region Components
  public Animator Anim { get; private set; }
  public Rigidbody2D RB { get; private set; }
  public PlayerInputHandler InputHandler { get; private set; }
  public Core core { get; private set; }
  #endregion
  
  #region Check Transforms
  [SerializeField] private Transform groundCheck;
  public Transform boomerangPosition;
  #endregion

  #region Unity Functions
  private void Awake() {
    // Movement
    core = GetComponentInChildren<Core>();
    StateMachine = new PlayerStateMachine();
    IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
    MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
    JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
    DoubleJumpState = new PlayerDoubleJumpState(this, StateMachine, playerData, "inAir");
    DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
    InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
    LandState = new PlayerLandState(this, StateMachine, playerData, "land");
    BounceState = new PlayerBounceState(this, StateMachine, playerData, "bounce");
    RollState = new PlayerRollState(this, StateMachine, playerData, "roll");
    SleepState = new PlayerSleepState(this, StateMachine, playerData, "sleep");

    // Combat
    PrimaryGroundAttackState = new PlayerPrimaryGroundAttackState(this, StateMachine, playerData, "primaryGroundAttack");
    PrimaryAirAttackState = new PlayerPrimaryAirAttackState(this, StateMachine, playerData, "primaryAirAttack");
    SecondaryGroundAttackState = new PlayerSecondaryGroundAttackState(this, StateMachine, playerData, "secondaryGroundAttack");
    SecondaryAirAttackState = new PlayerSecondaryAirAttackState(this, StateMachine, playerData, "secondaryAirAttack");

    // Specials
    // Boomerang
    BoomerangThrowState = new PlayerBoomerangThrowState(this, StateMachine, playerData, "boomerangThrow");
    BoomerangCatchState = new PlayerBoomerangCatchState(this, StateMachine, playerData, "boomerangCatch");
    // Shield
    StartBlockState = new PlayerStartBlockState(this, StateMachine, playerData, "startBlock");
    BlockState = new PlayerBlockState(this, StateMachine, playerData, "block");
    EndBlockState = new PlayerEndBlockState(this, StateMachine, playerData, "endBlock");
    // JetPack
    // Glider
    GlideState = new PlayerGlideState(this, StateMachine, playerData, "glide");
  }

  private void Start() {
    Anim = GetComponent<Animator>();
    RB = GetComponent<Rigidbody2D>();
    InputHandler = GetComponent<PlayerInputHandler>();

    StateMachine.Initialize(IdleState);
  }

  private void Update() {
    core.LogicUpdate();

    StateMachine.CurrentState.LogicUpdate();
  }

  private void FixedUpdate() {
    StateMachine.CurrentState.PhysicsUpdate();
  }
  #endregion

  #region Other Functions
  private void AnimationTrigger() {
    StateMachine.CurrentState.AnimationTrigger();
  }

  private void AnimationFinishTrigger() {
    StateMachine.CurrentState.AnimationFinishTrigger();
  }
  #endregion

  #region Gizmos
  public void OnDrawGizmos() {
    StateMachine.CurrentState.DrawGizmos();
  }
  #endregion
}
