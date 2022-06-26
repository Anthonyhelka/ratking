using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject {
  // [Header("Specials")]
  public enum Special { boomerang, shield, jetPack, glider };
  public Special selectedSpecial = Special.boomerang;

  [Header("Idle State")]
  public float sleepTime = 5.0f;
  
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

  [Header("Dash State")]
  public float dashCooldown = 0.5f;
  public float dashTime = 0.2f;
  public float dashVelocity = 30.0f;
  public float drag = 10.0f;
  public float dashEndYMultiplier = 0.2f;

  [Header("Bounce State")]
  public float bounceVelocity = 4.0f;

  [Header("Roll State")]
  public float rollVelocity = 2.0f;

  [Header("Primary Ground Attack State")]
  public float[] primaryGroundAttackDamage = new float[2];
  public float[] primaryGroundAttackRadius = new float[2];
  public float[] primaryGroundAttackMovement = new float[2];
  public float primaryGroundAttackCooldown = 0.0f;

  [Header("Primary Air Attack State")]
  public float primaryAirAttackDamage = 20.0f;
  public float primaryAirAttackRadius = 0.3f;
  public float primaryAirAttackCooldown = 0.0f;

  [Header("Secondary Ground Attack State")]
  public float secondaryGroundAttackDamage = 20.0f;
  public float secondaryGroundAttackRadius = 0.2f;
  public float secondaryGroundAttackXVelocity = 0.5f;
  public float secondaryGroundAttackYVelocity = 0.3f;
  public float secondaryGroundAttackCooldown = 1.0f;

  [Header("Secondary Air Attack State")]
  public float secondaryAirAttackDamage = 20.0f;
  public float secondaryAirAttackRadius = 0.3f;
  public float secondaryAirAttackXVelocity = 1.5f;
  public float secondaryAirAttackYVelocity = 0.0f;
  public float secondaryAirAttackCooldown = 1.0f;

  [Header("Boomerang Throw State")]
  public GameObject boomerang;
  public int boomerangDamage = 10;
  public float boomerangVelocity = 4.0f;
  public float boomerangTravelDistance = 2.5f;
  public float boomerangCooldown = 1.0f;

  [Header("Glide State")]
  public float glideXVelocity = 2.0f;
  public float glideYVelocity = 0.25f;

  [Header("Block State")]
  public float blockXVelocity = 0.5f;

  [Header("Health")]
  public int maxHealth = 5;
  public float invincibilityTimer = 1.0f;
}
