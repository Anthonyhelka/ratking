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
  public PlayerDashState DashState { get; private set; }
  public PlayerInAirState InAirState { get; private set; }
  public PlayerLandState LandState { get; private set; }
  public PlayerAttackState PrimaryAttackState { get; private set; }
  public PlayerAttackState SecondaryAttackState { get; private set; }
  [SerializeField] private PlayerData playerData;
  // Specials
  // Boomerang
  public PlayerBoomerangThrowState BoomerangThrowState { get; private set; }
  public PlayerBoomerangCatchState BoomerangCatchState { get; private set; }
  // Shield
  public PlayerBlockState BlockState { get; private set; }
  // JetPack
  // Glider
  public PlayerGlideState GlideState { get; private set; }
  #endregion
  
  #region Components
  public Animator Anim { get; private set; }
  public Rigidbody2D RB { get; private set; }
  public PlayerInputHandler InputHandler { get; private set; }
  public PlayerInventory Inventory { get; private set; }
  public Core core { get; private set; }
  #endregion
  
  #region Check Transforms
  [SerializeField] private Transform groundCheck;
  public Transform boomerangPosition;
  #endregion

  #region Unity Functions
  private void Awake() {
    core = GetComponentInChildren<Core>();
    StateMachine = new PlayerStateMachine();
    IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
    MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
    JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
    DoubleJumpState = new PlayerDoubleJumpState(this, StateMachine, playerData, "inAir");
    DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
    InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
    LandState = new PlayerLandState(this, StateMachine, playerData, "land");
    PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
    SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
    // Specials
    // Boomerang
    BoomerangThrowState = new PlayerBoomerangThrowState(this, StateMachine, playerData, "boomerangThrow");
    BoomerangCatchState = new PlayerBoomerangCatchState(this, StateMachine, playerData, "boomerangCatch");
    // Shield
    BlockState = new PlayerBlockState(this, StateMachine, playerData, "block");
    // JetPack
    // Glider
    GlideState = new PlayerGlideState(this, StateMachine, playerData, "glide");
  }

  private void Start() {
    Anim = GetComponent<Animator>();
    RB = GetComponent<Rigidbody2D>();
    InputHandler = GetComponent<PlayerInputHandler>();
    Inventory = GetComponent<PlayerInventory>();

    PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
    // SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);

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
  public virtual void OnDrawGizmos() {
    core.DrawGizmos();
  }
  #endregion
}
