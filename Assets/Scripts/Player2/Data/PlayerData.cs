using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject {
  [Header("Idle State")]

  [Header("Move State")]
  public float movementVelocity = 1.5f;

  [Header("In Air State")]
  public float coyoteTime = 0.2f;
  public float variableJumpHeightMultiplier = 0.5f;

  [Header("Jump State")]
  public float jumpVelocity = 4.0f;
  public int amountOfJumps = 1;

  [Header("Double Jump State")]
  public float doubleJumpVelocity = 4.0f;

  [Header("Check Variables")]
  public float groundCheckRadius = 0.3f;
  public LayerMask whatIsGround;

  // [Header("Land State")]
}
