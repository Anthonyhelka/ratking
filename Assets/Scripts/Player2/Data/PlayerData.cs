using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject {
  // [Header("Specials")]
  public enum Special { bananaSlippers, boomerang, plungeform };
  public Special selectedSpecial = Special.bananaSlippers;
  // [Header("Idle State")]

  [Header("Move State")]
  public float movementVelocity = 1.5f;

  [Header("In Air State")]
  public float coyoteTime = 0.2f;
  public float variableJumpHeightMultiplier = 0.5f;

  [Header("Jump State")]
  public float jumpVelocity = 4.0f;
  public int amountOfJumps = 2;

  [Header("Double Jump State")]
  public float doubleJumpVelocity = 4.0f;

  // [Header("Land State")]

  [Header("Wall Slide State")]
  public float wallSlideVelocity = 0.75f;

  // [Header("Wall Grab State")]

  [Header("Wall Climb State")]
  public float wallClimbVelocity = 0.75f;
  
  [Header("Banana Slippers Idle State")]
  public float bananaSlippersSlipMultiplier = 0.9f;

  [Header("Banana Slippers Move State")]
  public float bananaSlippersMovementVelocity = 2.0f;

  [Header("Boomerang Throw State")]
  public GameObject boomerang;
  public int boomerangDamage = 10;
  public float boomerangSpeed = 4.0f;
  public float boomerangTravelDistance = 2.5f;
  public float boomerangCooldown = 1.0f;

  [Header("Plungeform Throw State")]
  public GameObject plungeform;
  public int plungeformDamage = 10;
  public float plungeformSpeed = 4.0f;
  public float plungeformTravelDistance = 2.5f;
  public float plungeformCooldown = 1.0f;

  [Header("Check Variables")]
  public float groundCheckRadius = 0.075f;
  public LayerMask whatIsGround;
  public float wallCheckDistance = 0.1f;
}
