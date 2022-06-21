using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject {
  // [Header("Specials")]
  public enum Special { boomerang, shield, jetPack, glider };
  public Special selectedSpecial = Special.boomerang;
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

  [Header("Boomerang Throw State")]
  public GameObject boomerang;
  public int boomerangDamage = 10;
  public float boomerangSpeed = 4.0f;
  public float boomerangTravelDistance = 2.5f;
  public float boomerangCooldown = 1.0f;

  [Header("Check Variables")]
  public float groundCheckRadius = 0.075f;
  public LayerMask whatIsGround;
}
